using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBotCore.Models
{
    public class EdgeFollowedBy
    {
        public int count { get; set; }
    }

    public class EdgeFollow
    {
        public int count { get; set; }
    }

    public class EdgeMutualFollowedBy
    {
        public int count { get; set; }
        public List<object> edges { get; set; }
    }

    public class PageInfo
    {
        public bool has_next_page { get; set; }
        public object end_cursor { get; set; }
    }

    public class EdgeFelixVideoTimeline
    {
        public int count { get; set; }
        public PageInfo page_info { get; set; }
        public List<object> edges { get; set; }
    }

    public class PageInfo2
    {
        public bool has_next_page { get; set; }
        public object end_cursor { get; set; }
    }

    public class EdgeOwnerToTimelineMedia
    {
        public int count { get; set; }
        public PageInfo2 page_info { get; set; }
        public List<object> edges { get; set; }
    }

    public class PageInfo3
    {
        public bool has_next_page { get; set; }
        public object end_cursor { get; set; }
    }

    public class EdgeSavedMedia
    {
        public int count { get; set; }
        public PageInfo3 page_info { get; set; }
        public List<object> edges { get; set; }
    }

    public class PageInfo4
    {
        public bool has_next_page { get; set; }
        public object end_cursor { get; set; }
    }

    public class EdgeMediaCollections
    {
        public int count { get; set; }
        public PageInfo4 page_info { get; set; }
        public List<object> edges { get; set; }
    }

    public class User
    {
        public string biography { get; set; }
        public bool blocked_by_viewer { get; set; }
        public object restricted_by_viewer { get; set; }
        public bool country_block { get; set; }
        public string external_url { get; set; }
        public string external_url_linkshimmed { get; set; }
        public EdgeFollowedBy edge_followed_by { get; set; }
        public bool followed_by_viewer { get; set; }
        public EdgeFollow edge_follow { get; set; }
        public bool follows_viewer { get; set; }
        public string full_name { get; set; }
        public bool has_ar_effects { get; set; }
        public bool has_channel { get; set; }
        public bool has_blocked_viewer { get; set; }
        public int highlight_reel_count { get; set; }
        public bool has_requested_viewer { get; set; }
        public string id { get; set; }
        public bool is_business_account { get; set; }
        public bool is_joined_recently { get; set; }
        public object business_category_name { get; set; }
        public object category_id { get; set; }
        public object overall_category_name { get; set; }
        public bool is_private { get; set; }
        public bool is_verified { get; set; }
        public EdgeMutualFollowedBy edge_mutual_followed_by { get; set; }
        public string profile_pic_url { get; set; }
        public string profile_pic_url_hd { get; set; }
        public bool requested_by_viewer { get; set; }
        public string username { get; set; }
        public object connected_fb_page { get; set; }
        public EdgeFelixVideoTimeline edge_felix_video_timeline { get; set; }
        public EdgeOwnerToTimelineMedia edge_owner_to_timeline_media { get; set; }
        public EdgeSavedMedia edge_saved_media { get; set; }
        public EdgeMediaCollections edge_media_collections { get; set; }
    }

    public class Graphql
    {
        public User user { get; set; }
    }

    public class InstaRoot
    {
        public string logging_page_id { get; set; }
        public bool show_suggested_profiles { get; set; }
        public bool show_follow_dialog { get; set; }
        public Graphql graphql { get; set; }
        public object toast_content_on_load { get; set; }
    }
}
