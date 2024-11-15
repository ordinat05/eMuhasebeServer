using System.Diagnostics;

namespace eMuhasebeServer.WebAPI.Middlewares
{
    public class RequestRateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly long _maximumBytesPerSecond;

        public RequestRateLimitingMiddleware(RequestDelegate next, long maximumBytesPerSecond)
        {
            _next = next;
            _maximumBytesPerSecond = maximumBytesPerSecond;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBody = context.Request.Body;
            var rateLimitedBody = new RateLimitedStream(originalBody, _maximumBytesPerSecond);
            context.Request.Body = rateLimitedBody;

            await _next(context);
        }
    }

    public class RateLimitedStream : Stream
    {
        private readonly Stream _innerStream;
        private readonly long _maximumBytesPerSecond;
        private long _totalBytesRead;
        private readonly Stopwatch _stopwatch;

        public RateLimitedStream(Stream innerStream, long maximumBytesPerSecond)
        {
            _innerStream = innerStream;
            _maximumBytesPerSecond = maximumBytesPerSecond;
            _totalBytesRead = 0;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public override bool CanRead => _innerStream.CanRead;
        public override bool CanSeek => _innerStream.CanSeek;
        public override bool CanWrite => _innerStream.CanWrite;
        public override long Length => _innerStream.Length;
        public override long Position
        {
            get => _innerStream.Position;
            set => _innerStream.Position = value;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            int bytesRead = await _innerStream.ReadAsync(buffer, offset, count, cancellationToken);
            _totalBytesRead += bytesRead;
            double elapsedSeconds = _stopwatch.Elapsed.TotalSeconds;
            long allowedBytes = (long)(elapsedSeconds * _maximumBytesPerSecond);
            if (_totalBytesRead > allowedBytes)
            {
                long excessBytes = _totalBytesRead - allowedBytes;
                int delayMilliseconds = (int)((excessBytes * 1000) / _maximumBytesPerSecond);
                await Task.Delay(delayMilliseconds, cancellationToken);
            }
            return bytesRead;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Synchronous read is not supported. Use ReadAsync instead.");
        }

        public override long Seek(long offset, SeekOrigin origin) => _innerStream.Seek(offset, origin);
        public override void SetLength(long value) => _innerStream.SetLength(value);
        public override void Write(byte[] buffer, int offset, int count) => _innerStream.Write(buffer, offset, count);
        public override void Flush() => _innerStream.Flush();
    }
}
