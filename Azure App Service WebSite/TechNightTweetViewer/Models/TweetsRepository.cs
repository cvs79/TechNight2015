using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TechNightTweetViewer.Models
{
    public class TweetsRepository
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public IEnumerable<TechNightTweet> GetAllTweets()
        {
            var techNightTweets = new List<TechNightTweet>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [ID], [Tweet], [Tweeter] FROM [dbo].[TechNightTweet]", connection))
                {
                    command.Notification = null;
                    
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        techNightTweets.Add(new TechNightTweet
                        {
                            ID = reader.GetInt32(0),
                            Tweet = reader.GetString(1),
                            Tweeter = reader.GetString(2)
                        });
                    }
                }
            }
            return techNightTweets;
        }        
    }
}