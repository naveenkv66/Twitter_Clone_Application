using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TweetManager
    {
        public List<Tweet> GetTweetsByUserId(int userId)
        {
            using (var db = new MVCApplicationEntities())
            {
                return db.Tweets.Where(item => item.User_Id == userId.ToString()).ToList();
            }
        }

        public List<Tweet> GetFollwersTweets(string userId)
        {
            using (var db = new MVCApplicationEntities())
            {
                var tweets = (from peopl in db.People
                              join following in db.Followings
                              on peopl.User_Id equals following.User_Id
                              join tweet in db.Tweets
                              on following.Following_Id equals tweet.User_Id
                              select tweet).ToList();
                return tweets;
            }
        }

        public void Save(Tweet tweet)
        {
            using (var db = new MVCApplicationEntities())
            {
                db.Tweets.Add(tweet);
                db.SaveChanges();
            }
        }

        public void Update(Tweet tweet)
        {
            using (var db = new MVCApplicationEntities())
            {
                var updateTweet = db.Tweets.FirstOrDefault(item => item.Tweet_Id == tweet.Tweet_Id);
                updateTweet.Message = tweet.Message;
                db.SaveChanges();

            }
        }

        public void DeleteTweet(int tweetId)
        {
            using (var db = new MVCApplicationEntities())
            {
                var deleteTweet = db.Tweets.FirstOrDefault(item => item.Tweet_Id == tweetId);
                db.Tweets.Remove(deleteTweet);
                db.SaveChanges();

            }
        }
    }
}
