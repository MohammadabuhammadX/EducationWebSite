using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GeneralBLL
    {
        GeneralDAO dao = new GeneralDAO();
        AdsDAO adsdao = new AdsDAO();
        public GeneralDTO GetAllPosts()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.SliderPost = dao.GetSliderPosts();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.PopularPost = dao.GetPopularPost();
            dto.MostViewedPost = dao.GetMostViewedPost();
            dto.Videos = dao.GetVideos();
            dto.AdsList = adsdao.GetAds();
            return dto;
        }
        CategoryDAO categorydao = new CategoryDAO();
        public GeneralDTO GetCategoryPostList(string categoryName)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost =dao.GetBreakingPosts();
            dto.AdsList = adsdao.GetAds();
            if (categoryName == "Video")
            {
                dto.Videos = dao.GetAllVideos();
                dto.CategoryName = "Video";
            }
            else       // for GetCategoryPost we need categoryID ,So first we've got to find the CategoryID and let's GetCategores in CategoryID
            {
                List<CategoryDTO> categorylist = categorydao.GetCategories();
                int categoryID = 0;
                foreach (var item in categorylist)
                {
                    if (categoryName == SeoLink.GenerateUrl(item.CategoryName))
                    {
                        categoryID = item.ID;
                        dto.CategoryName = item.CategoryName;
                        break;
                    }
                }
                dto.CategoryPostList = dao.GetCategoryPostList(categoryID);
            }
            return dto;
        }

        public GeneralDTO GetPostDetailPageItemWithID(int ID)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost=dao.GetBreakingPosts();
            dto.AdsList =adsdao.GetAds();
            dto.PostDetail = dao.GetPostDetail(ID);
            return dto;
        }
        AddressDAO addressdao=new AddressDAO();
        public GeneralDTO GetContactpageItems()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.AdsList = adsdao.GetAds();
            dto.Address=addressdao.GetAddresses().First();
            return dto;
        }

        public GeneralDTO GetSearchPosts(string searchText)
        {
           GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.AdsList=adsdao.GetAds();
            dto.CategoryPostList = dao.GetSearchPost(searchText);
            dto.SearchText = searchText;
            return dto;
        }
    }
}
