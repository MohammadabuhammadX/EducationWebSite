using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LayoutBLL
    {
        CategoryDAO categortydao = new CategoryDAO(); // As you may remember in the category pages, we have created a method to get categories so we can call
        SocialMediaDAO socialdao = new SocialMediaDAO();
        MetaDAO metadao = new MetaDAO();
        FavDAO favdao = new FavDAO();
        AddressDAO addressdao = new AddressDAO();
        PostDAO postdao = new PostDAO();
        public HomeLayoutDTO GetLayoutData()
        {
            HomeLayoutDTO dto = new HomeLayoutDTO();
            dto.Categories = categortydao.GetCategories();
            List<SocialMediaDTO> socialmedialist = new List<SocialMediaDTO>();   //We'll get all SocialMedia with a method
            socialmedialist = socialdao.GetSocialMedias();                        // now in SocialMediaList Video ,we've already defined a method to grab all that socail media list 
            dto.Facebook = socialmedialist.First(x => x.Link.Contains("facebook"));
            dto.Twitter = socialmedialist.First(x => x.Link.Contains("twitter"));
            dto.Instagram = socialmedialist.First(x => x.Link.Contains("instagram"));
            dto.Youtube = socialmedialist.First(x => x.Link.Contains("youtube"));
            dto.Linkedin = socialmedialist.First(x => x.Link.Contains("linkedin"));
            dto.FavDTO=favdao.GetFav();
            dto.MetaList = metadao.GetMetaData();
            List<AddressDTO> addresslist=addressdao.GetAddresses();
            dto.Address=addresslist.First();
            dto.HotNews = postdao.GetHotNews();

            return dto;
        }
    }
}
