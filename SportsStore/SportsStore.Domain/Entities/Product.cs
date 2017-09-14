using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace SportsStore.Domain.Entities
{

    public class Product
    {

        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please entrer a product name")]
        public string Name { get; set; }

        //[DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a postive price")]
        public decimal Price { get; set; }


        // This is used to demonstrate a failed Can_Create_Categories Unit Test
        [Required(ErrorMessage = "Please specify a category")]
        private string category;
        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
            }
        }



        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }


    }
}