//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KingIT
{
    using System;
    using System.Collections.Generic;
    
    public partial class Store_Centers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store_Centers()
        {
            this.Pavilions = new HashSet<Pavilions>();
        }
    
        public int ID_SC { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int Quantity_pavilions { get; set; }
        public string City { get; set; }
        public decimal Cost { get; set; }
        public double Cofficient_of_added_value { get; set; }
        public int Number_of_floors { get; set; }
        public byte[] Image { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pavilions> Pavilions { get; set; }
        public virtual Status_SC Status_SC { get; set; }
    }
}
