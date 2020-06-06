using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweeter.Contract
{
    public static class ApiRoutes
    {
        public  const string Root = "api";
        public  const string Version = "v1";
        public  const string Base = Root+"/"+Version;

        public static class User
        {
            public const string Register = Base + "/user/register";
            public const string Delete = Base + "/user/delete";
            public const string Login = Base + "/user/login";
            public const string Update = Base + "/user/update";
            public const string Get = Base + "/user/get";
            public const string GetAll = Base + "/user/get";
        }

        public static class Comment
        {
            public const string PostComment = Base + "/comment/post";
            public const string Delete = Base + "/comment/delete/{commentId}";
            public const string GetByTweetId = Base + "/comment/getByTweetId/{tweetId}";
            public const string GetById = Base + "/comment/GetById/{commentId}";
        }

        public static class Tweet
        {
            public const string PostTweet = Base + "/tweet/post";
            public const string GetTweet = Base + "/tweet/get/{tweetId}";
            public const string GetTweetsByUser = Base + "/tweet/getByUser/{UserId}";
            public const string GetAllTweets = Base + "/tweet/get";
            public const string DeleteTweet = Base + "/tweet/delete/{tweetId}";
        }

        public static class Like
        {
            public const string LikeTweet = Base + "/Like/likeTweet/{tweetId}";
            public const string LikeComment = Base + "/Like/likeComment/{commentId}";
            public const string GetTweetLikes = Base + "/Like/getTweetLikes/{commentId}";
            public const string GetCommentLikes = Base + "/Like/getCommentLikes/{commentId}";
        }
    }
}
