using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GeneralDAO : PostContext
    {
        public List<VideooDTO> GetAllVideos()
        {
            List<VideooDTO> dtolist = new List<VideooDTO>();
            List<Video> list = db.Videos.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).ToList();
            foreach (var item in list)
            {
                VideooDTO dto = new VideooDTO();
                dto.ID = item.ID;
                dto.Title = item.Title;
                dto.VideoPath = item.VideoPath;
                dto.OriginalVideoPath = item.OriginalVideoPath;
                dto.AddDate = item.AddDate;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetBreakingPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.Slider == false && x.isDeleted == false)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            Title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            Adddate = p.AddDate
                        }

                       ).OrderByDescending(x => x.Adddate).Take(5).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.Title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dto.AddDate = item.Adddate;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetCategoryPostList(int categoryID)
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.isDeleted == false && x.CategoryID==categoryID)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            Title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            Adddate = p.AddDate
                        }

                       ).OrderByDescending(x => x.Adddate).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.Title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dto.AddDate = item.Adddate;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetMostViewedPost()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.isDeleted == false)  //I can add a time conditon for a week or for a month.
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            Title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            Adddate = p.AddDate
                        }

                       ).OrderByDescending(x => x.viewcount).Take(5).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.Title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dto.AddDate = item.Adddate;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetPopularPost()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.isDeleted == false && x.Area2 == true)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            Title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            Adddate = p.AddDate
                        }

                       ).OrderByDescending(x => x.Adddate).Take(5).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.Title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dto.AddDate = item.Adddate;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public PostDTO GetPostDetail(int ID)
        {        // We have to increment the view count for each select
            Post post = db.Posts.First(x => x.ID == ID);
            post.ViewCount++;
            db.SaveChanges();
            PostDTO dto = new PostDTO();
            dto.ID = post.ID;
            dto.Title = post.Title;
            dto.ShortContent = post.ShortContent;
            dto.PostContent = post.PostContent;
            dto.Language = post.LanguageName;
            dto.SeoLink = post.SeoLink;
            dto.CategoryID = post.CategoryID;
            dto.CategoryName = (db.Categories.First(x => x.ID == dto.CategoryID)).CategoryName;
            List<PostImage> images = db.PostImages.Where(x => x.isDeleted == false && x.PostID == ID).ToList();
            List<PostImageDTO> imagestolist = new List<PostImageDTO>();
            foreach (var item in images)
            {
                PostImageDTO img = new PostImageDTO();
                img.ID = item.ID;
                img.ImagePath = item.ImagePath;
                imagestolist.Add(img);

            }
            dto.PostImages = imagestolist;
            dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == ID && x.isApproved == true).Count();
            List<Comment> comments = db.Comments.Where(x => x.isDeleted == false && x.PostID == ID && x.isApproved == true).ToList();
            List<CommentDTO> commentdtolist = new List<CommentDTO>(0);
            foreach (var item in comments)
            {
                CommentDTO cdto = new CommentDTO();
                cdto.ID = item.ID;
                cdto.AddDate = item.AddDate;
                cdto.CommentContent = item.CommentContent;
                cdto.Name = item.NameSurname;
                cdto.Email = item.Email;
                commentdtolist.Add(cdto);
            }
            dto.CommentList=commentdtolist;
            List<PostTag> tags =db.PostTags.Where(x=>x.isDeleted==false && x.PostID == ID).ToList();
            List<TagDTO> taglist = new List<TagDTO>();
            foreach (var item in tags)
            {
                TagDTO tdto = new TagDTO();
                tdto.ID = item.ID;
                tdto.TagContent= item.TagContent;
                taglist.Add(tdto);

            }
            dto.TagList = taglist;
            return dto;
        }

        public List<PostDTO> GetSearchPost(string searchText)
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.isDeleted == false &&x.Title.Contains(searchText) ||x.PostContent.Contains(searchText))
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            Title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            Adddate = p.AddDate
                        }

                       ).OrderByDescending(x => x.Adddate).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.Title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dto.AddDate = item.Adddate;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetSliderPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.Slider == true && x.isDeleted == false)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            Title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            Adddate = p.AddDate
                        }

                       ).OrderByDescending(x => x.Adddate).Take(8).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.Title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dto.AddDate = item.Adddate;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<VideooDTO> GetVideos()
        {
            List<VideooDTO> dtolist = new List<VideooDTO>();
            List<Video> list = db.Videos.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).Take(3).ToList();
            foreach (var item in list)
            {
                VideooDTO dto = new VideooDTO();
                dto.ID = item.ID;
                dto.Title = item.Title;
                dto.VideoPath = item.VideoPath;
                dto.OriginalVideoPath = item.OriginalVideoPath;
                dto.AddDate = item.AddDate;
                dtolist.Add(dto);
            }
            return dtolist;
        }
    }
}
/*
 And just like we made in layout , we will generate category and post tables,
We're going to show category names as a URL in the following videos
 
 */