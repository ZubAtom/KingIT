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
    
    public partial class Employers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employers()
        {
            this.Rents = new HashSet<Rents>();
        }
    
        public int ID_employer { get; set; }
        public string Secondname { get; set; }
        public string Firstname { get; set; }
        public string Fathername { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Nullable<int> Role { get; set; }
        public string Phone_number { get; set; }
        public string Gender { get; set; }
        public byte[] Photo { get; set; }
    
        public virtual Roles Roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rents> Rents { get; set; }
    }
}
