//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zoo.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class InformationReptiles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InformationReptiles()
        {
            this.AdditionalInformation = new HashSet<AdditionalInformation>();
        }
    
        public int RecordingId { get; set; }
        public string HibernationPeriod { get; set; }
        public string NormalTemperature { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdditionalInformation> AdditionalInformation { get; set; }
    }
}