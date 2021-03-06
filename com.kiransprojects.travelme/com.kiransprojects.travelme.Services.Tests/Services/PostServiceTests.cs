﻿namespace com.kiransprojects.travelme.Services.Tests.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Interfaces;
    using com.kiransprojects.travelme.Services.Services;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// PostService Tests
    /// </summary>
    [TestFixture]
    public class PostServiceTests
    {
        /// <summary>
        /// Ensures all posts for a trip can be returned
        /// </summary>
        [Test]
        public void GetPosts_PostsExist_ReturnsList()
        {
            IList<Post> posts = new List<Post>();

            for (int i = 0; i < 2; i++)
            {
                Post post = new Post();
                post.ID = Guid.NewGuid();
                post.PostData = "data";
                post.PostDate = DateTime.Now;
                posts.Add(post);
            }

            Mock<IRepository<Post>> repository = new Mock<IRepository<Post>>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<IMediaService> mediaService = new Mock<IMediaService>();
            Mock<IFileService> fileService = new Mock<IFileService>(); 

            repository.Setup(o => o.GetAll()).Returns(posts);

            PostService service = new PostService(repository.Object, loggerService.Object, mediaService.Object, fileService.Object);

            IList<Post> result = service.GetPosts();
            Assert.AreEqual(posts.Count, result.Count);
        }

        /// <summary>
        /// Ensures null is returned when posts does not exist
        /// </summary>
        [Test]
        public void GetPosts_PostNonExist_ReturnsNull()
        {
            Mock<IRepository<Post>> repository = new Mock<IRepository<Post>>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<IMediaService> mediaService = new Mock<IMediaService>();
            Mock<IFileService> fileService = new Mock<IFileService>();

            repository.SetupSequence(o => o.GetAll()).Returns(null);

            PostService service = new PostService(repository.Object, loggerService.Object, mediaService.Object, fileService.Object);

            IList<Post> result = service.GetPosts();
            Assert.IsNull(result);
        }

        /// <summary>
        /// Ensures exising post 
        /// </summary>
        [Test]
        public void GetPost_ExistingPost_ReturnsPost()
        {
            Post post = new Post();
            post.ID = Guid.NewGuid();
            post.PostData = "data";

            Mock<IRepository<Post>> repository = new Mock<IRepository<Post>>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<IMediaService> mediaService = new Mock<IMediaService>();
            Mock<IFileService> fileService = new Mock<IFileService>();

            repository.SetupSequence(o => o.GetByID(post.ID)).Returns(post);

            PostService service = new PostService(repository.Object, loggerService.Object, mediaService.Object, fileService.Object);
            Post result = service.GetPost(post.ID);
            Assert.AreEqual(post, result);
        }

        /// <summary>
        /// Ensures non existing post returns null
        /// </summary>
        [Test]
        public void GetPost_NonExistingPost_ReturnsNull()
        {
            Mock<IRepository<Post>> repository = new Mock<IRepository<Post>>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<IMediaService> mediaService = new Mock<IMediaService>();
            Mock<IFileService> fileService = new Mock<IFileService>();

            repository.SetupSequence(o => o.GetByID(It.IsAny<Guid>())).Returns(null);

            PostService service = new PostService(repository.Object, loggerService.Object, mediaService.Object, fileService.Object);
            Post result = service.GetPost(Guid.NewGuid());
            Assert.IsNull(result);
        }

        /// <summary>
        /// Ensures posts can be added
        /// </summary>
        [Test]
        public void AddPost_ValidPost_TrueFlag()
        {
            Post post = new Post();
            post.ID = Guid.NewGuid();
            post.PostData = "data";

            Mock<IRepository<Post>> repository = new Mock<IRepository<Post>>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<IMediaService> mediaService = new Mock<IMediaService>();
            Mock<IFileService> fileService = new Mock<IFileService>();

            repository.Setup(o => o.Insert(post));

            PostService service = new PostService(repository.Object, loggerService.Object, mediaService.Object, fileService.Object);
            bool flag = service.AddPost(post);

            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Ensures null posts cannot be passed
        /// </summary>
        [Test]
        public void AddPost_NullPost_FalseFlag()
        {
            Mock<IRepository<Post>> repository = new Mock<IRepository<Post>>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<IMediaService> mediaService = new Mock<IMediaService>();
            Mock<IFileService> fileService = new Mock<IFileService>();

            PostService service = new PostService(repository.Object, loggerService.Object, mediaService.Object, fileService.Object);
            bool flag = service.AddPost(null);
            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures a post can be edited
        /// </summary>
        [Test]
        public void EditPost_ValidPost_TrueFlag()
        {
            Post post = new Post();
            post.ID = Guid.NewGuid();
            post.PostData = "data"; 

            Mock<IRepository<Post>> repository = new Mock<IRepository<Post>>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<IMediaService> mediaService = new Mock<IMediaService>();
            Mock<IFileService> fileService = new Mock<IFileService>();

            PostService service = new PostService(repository.Object, loggerService.Object, mediaService.Object, fileService.Object);
            bool flag = service.EditPost(post);
            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Ensures returns false with null post
        /// </summary>
        [Test]
        public void EditPost_NullPost_FalseFlag()
        {
            Mock<IRepository<Post>> repository = new Mock<IRepository<Post>>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<IMediaService> mediaService = new Mock<IMediaService>();
            Mock<IFileService> fileService = new Mock<IFileService>();

            PostService service = new PostService(repository.Object, loggerService.Object, mediaService.Object, fileService.Object);
            bool flag = service.EditPost(null);
            Assert.IsFalse(flag); 
        }

        /// <summary>
        /// Ensures posts can be deleted
        /// </summary>
        [Test]
        public void DeletePost_ValidPost_TrueFlag()
        {
            Post post = new Post();
            post.ID = Guid.NewGuid();
            post.PostData = "data"; 

            Mock<IRepository<Post>> repository = new Mock<IRepository<Post>>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<IMediaService> mediaService = new Mock<IMediaService>();
            Mock<IFileService> fileService = new Mock<IFileService>();

            repository.Setup(o => o.Delete(post));

            PostService service = new PostService(repository.Object, loggerService.Object, mediaService.Object, fileService.Object);
            bool flag = service.DeletePost(post);
            Assert.IsTrue(flag); 
        }

        /// <summary>
        /// Ensures return null when post is null
        /// </summary>
        [Test]
        public void DeletePost_NullPost_FalseFlag()
        {
            Mock<IRepository<Post>> repository = new Mock<IRepository<Post>>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<IMediaService> mediaService = new Mock<IMediaService>();
            Mock<IFileService> fileService = new Mock<IFileService>();

            repository.Setup(o => o.Delete(null));

            PostService service = new PostService(repository.Object, loggerService.Object, mediaService.Object, fileService.Object);
            bool flag = service.DeletePost(null);
            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures photos can be stored and related to the post
        /// </summary>
        [Test]
        public void AddPhoto_ValidPost_MediaStoredAndRetrived()
        {
            Mock<IRepository<Post>> repository = new Mock<IRepository<Post>>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<IMediaService> mediaService = new Mock<IMediaService>();
            Mock<IFileService> fileService = new Mock<IFileService>();

            mediaService.Setup(o => o.StoreMedia(It.IsAny<Media>())); 
            fileService.Setup(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true); 

            PostService service = new PostService(repository.Object, loggerService.Object, mediaService.Object, fileService.Object);
            byte[] test = new byte[3]; 
            Media stored = service.AddPhoto(Guid.NewGuid(), "test" ,test);

            StringAssert.StartsWith("test", stored.MediaData); 
        }

        /// <summary>
        /// Ensures an file service return false reutuns null
        /// </summary>
        [Test]
        public void AddPhoto_FailFileService_MediaNull()
        {
            Mock<IRepository<Post>> repository = new Mock<IRepository<Post>>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<IMediaService> mediaService = new Mock<IMediaService>();
            Mock<IFileService> fileService = new Mock<IFileService>();

            fileService.Setup(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(false); 

            PostService service = new PostService(repository.Object, loggerService.Object, mediaService.Object, fileService.Object);
            byte[] test = new byte[3];
            Media stored = service.AddPhoto(Guid.NewGuid(), "test", null);

            Assert.IsNull(stored); 
        }
    }
}