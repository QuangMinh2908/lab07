using System.ComponentModel.DataAnnotations.Schema;

namespace _25DH110979_quangminh.Models
{
    public partial class Customer
    {
        [NotMapped]
        public string FullName
        {
            get => CustomerName;
            set => CustomerName = value;
        }

        [NotMapped]
        public string Phone
        {
            get => CustomerPhone;
            set => CustomerPhone = value;
        }

        [NotMapped]
        public string Email
        {
            get => CustomerEmail;
            set => CustomerEmail = value;
        }

        [NotMapped]
        public string Address
        {
            get => CustomerAddress;
            set => CustomerAddress = value;
        }
    }

    public partial class Order
    {
        [NotMapped]
        public string OrderStatus
        {
            get => PaymentStatus;
            set => PaymentStatus = value;
        }
    }
}
