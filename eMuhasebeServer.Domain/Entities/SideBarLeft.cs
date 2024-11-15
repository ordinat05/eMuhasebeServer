using eMuhasebeServer.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eMuhasebeServer.Domain.Entities
{
    public sealed class SideBarLeft : Entity
    {
        // GuidId ve IsDeleted özellikleri Entity sınıfından geliyor

        [Required]
        [StringLength(255)]
        public string? Name { get; set; }

        public bool IsExpanded { get; set; }

        [ForeignKey("ParentSideBarLeft")]
        public Guid? ParentId { get; set; }

        public int Order { get; set; }
        [StringLength(200)]
        public string? IconCss { get; set; }
        public SideBarLeft? ParentSideBarLeft { get; set; }

        public ICollection<SideBarLeft> Children { get; set; } = new List<SideBarLeft>();

    }
}


// ParentId
//    ParentId: Üst menü öğesinin Id'si (kök öğeler için NULL)
// Order
//      Order: Aynı seviyedeki öğeler arasındaki sıralama