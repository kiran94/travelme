namespace com.kiransprojects.travelme.Services.Tests.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
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
            repository.Setup(o => o.GetAll()).Returns(posts); 

            PostService service = new PostService(repository.Object);
            
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
            repository.SetupSequence(o => o.GetAll()).Returns(null);

            PostService service = new PostService(repository.Object);

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
            repository.SetupSequence(o => o.GetByID(post.ID)).Returns(post);

            PostService service = new PostService(repository.Object);
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
            repository.SetupSequence(o => o.GetByID(It.IsAny<Guid>())).Returns(null);

            PostService service = new PostService(repository.Object);
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
            repository.Setup(o => o.Insert(post));

            PostService service = new PostService(repository.Object);
            bool flag = service.AddPost(post);

            Assert.IsTrue(flag);
        }

    }
}