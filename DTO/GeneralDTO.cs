using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GeneralDTO
    {
        public List<PostDTO> SliderPost { get; set; }   // for slighter posts , we will need a PostDTO list
        public List<PostDTO> PopularPost { get; set; }
        public List<PostDTO> MostViewedPost { get; set; }
        public List<PostDTO> BreakingPost { get; set; }
        public List<VideooDTO> Videos { get; set; }
        public List<AdsDTO> AdsList { get; set; }
        public PostDTO PostDetail { get; set; }

        // Now we can get data form the datebase
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string SearchText { get; set; }
        public int PostID { get; set; }
        public List<PostDTO> CategoryPostList { get; set; }
        public string CategoryName { get; set; }

        public AddressDTO Address { get; set; }   // In this project i have only one address,But if you have more than one address,You can can always add a list property


    }
}
