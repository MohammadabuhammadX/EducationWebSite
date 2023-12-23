using DTO;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;
using System.Web.Mvc;

namespace DAL
{
    public class CategoryDAO : PostContext
    {
        public static IEnumerable<SelectListItem> GetCategoriesForDropdown()
        {
            IEnumerable<SelectListItem> categoryList = db.Categories.Where(x=>x.isDeleted==false).OrderByDescending(x=>x.AddDate).Select(x=>new SelectListItem()
            {
                Text=x.CategoryName,
                Value=SqlFunctions.StringConvert((double)x.ID)
            }).ToList();
            return categoryList;
        }

        public List<Post> DeleteCategory(int ID)
        {
            try
            {
                Category ct = db.Categories.First(x => x.ID == ID);
                ct.isDeleted = true;
                ct.DeletedDate = DateTime.Now;
                ct.LastUpdateDate = DateTime.Now;
                ct.LastUpdateUserID =UserStatic.UserID;
                db.SaveChanges();
                List<Post> postlist = db.Posts.Where(x => x.isDeleted == false && x.CategoryID == ID).ToList();
                return postlist;
            }
            catch (Exception ex )
            {

                throw ex;
            }
        }

        public List<CategoryDTO> GetCategories()
        {
            List<Category> list =db.Categories.Where(x=>x.isDeleted==false).OrderByDescending(x=>x.AddDate).ToList();
            List<CategoryDTO> dtolist = new List<CategoryDTO>();
            foreach (var item in list)
            {
                CategoryDTO dto = new CategoryDTO();
                dto.CategoryName = item.CategoryName;
                dto.ID = item.ID;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public CategoryDTO GetCategoryWithID(int ID)
        {
            try
            {
                Category category = db.Categories.First(x => x.ID == ID);
                CategoryDTO dto = new CategoryDTO();
                dto.ID = category.ID;
                dto.CategoryName=category.CategoryName;
                return dto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int GetHashCode(Category category)
        {
			try
			{
				db.Categories.Add(category);
				db.SaveChanges();
				return category.ID;
			}
			catch (Exception ex)
			{

				throw ex;            // if you run the project and you go direct form url to AddCategory And Add Category will get this error
			}                       // So the reson for the errror is that we don't have any UserID information in the UserStatic class ,Because We set  the UserID information to user stati class after login operations
        }

        public void UpdateCategory(CategoryDTO model)
        {
            try
            {
                Category ct = db.Categories.First(x => x.ID == model.ID);
                ct.CategoryName = model.CategoryName;
                ct.LastUpdateDate = DateTime.Now;
                ct.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
