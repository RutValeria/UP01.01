//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UP01._01
{
    using System;
    using System.Collections.Generic;
    
    public partial class Requests
    {
        public int RequestID { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public Nullable<int> EquipmentID { get; set; }
        public Nullable<int> ProblemID { get; set; }
        public string ProblemDescription { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> StaffID { get; set; }
    
        public virtual Clients Clients { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual Problems Problems { get; set; }
        public virtual RequestStatuses RequestStatuses { get; set; }
        public virtual Staff Staff { get; set; }
    }
}