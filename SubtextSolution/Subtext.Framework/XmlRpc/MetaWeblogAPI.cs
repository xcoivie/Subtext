#region Disclaimer/Info
///////////////////////////////////////////////////////////////////////////////////////////////////
// Subtext WebLog
// 
// Subtext is an open source weblog system that is a fork of the .TEXT
// weblog system.
//
// For updated news and information please visit http://subtextproject.com/
// Subtext is hosted at SourceForge at http://sourceforge.net/projects/subtext
// The development mailing list is at subtext-devs@lists.sourceforge.net 
//
// This project is licensed under the BSD license.  See the License.txt file for more information.
///////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

using System;
using CookComputing.XmlRpc;

namespace Subtext.Framework.XmlRpc
{
	public struct BlogInfo
	{
		public string blogid;
		public string url;
		public string blogName;
	}


  // TODO: following attribute is a temporary workaround
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public struct Enclosure
  {
    public int length;
    public string type;
    public string url;
  }

  // TODO: following attribute is a temporary workaround
  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public struct Source
  {
    public string name;
    public string url;
  }

  [XmlRpcMissingMapping(MappingAction.Ignore)]
  public struct Post
  {
    [XmlRpcMissingMapping(MappingAction.Error)]
    [XmlRpcMember(Description="Required when posting.")]
    public DateTime dateCreated;
    [XmlRpcMissingMapping(MappingAction.Error)]
    [XmlRpcMember(Description="Required when posting.")]
    public string description;
    [XmlRpcMissingMapping(MappingAction.Error)]
    [XmlRpcMember(Description="Required when posting.")]
    public string title;
	[XmlRpcMember("categories", Description="Contains categories for the post.")]
    public string[] categories;
    public Enclosure enclosure;
    public string link;
    public string permalink;
    public string wp_slug;
    [XmlRpcMember(
      Description="Not required when posting. Depending on server may "
      + "be either string or integer. "
      + "Use Convert.ToInt32(postid) to treat as integer or "
      + "Convert.ToString(postid) to treat as string")]
    public object postid;       
    public Source source;
    public string userid;
  }

  public struct CategoryInfo
  {
    public string description;
    public string htmlUrl;
    public string rssUrl;
    public string title;
    public string categoryid;
  }

  public struct Category
  {
    public string categoryId;
    public string categoryName;
  }
    
  public struct mediaObject
  {
      public string name;
      public string type;
      public Byte[] bits;
  }
    
  public struct mediaObjectInfo
  {
      public string url;
  }

  public interface IMetaWeblog
  {
    [XmlRpcMethod("metaWeblog.editPost",Description="Updates and existing post to a designated blog "
      + "using the metaWeblog API. Returns true if completed.")]
     bool editPost(
      string postid,
      string username,
      string password,
      Post post,
      bool publish);

    [XmlRpcMethod("metaWeblog.getCategories",
      Description="Retrieves a list of valid categories for a post "
      + "using the metaWeblog API. Returns the metaWeblog categories "
      + "struct collection.")]
    CategoryInfo[] getCategories(
      string blogid,
      string username,
      string password);

    [XmlRpcMethod("metaWeblog.getPost",
      Description="Retrieves an existing post using the metaWeblog "
      + "API. Returns the metaWeblog struct.")]
    Post getPost(
      string postid,
      string username,
      string password);

    [XmlRpcMethod("metaWeblog.getRecentPosts",
      Description="Retrieves a list of the most recent existing post "
      + "using the metaWeblog API. Returns the metaWeblog struct collection.")]
    Post[] getRecentPosts(
      string blogid,
      string username,
      string password,
      int numberOfPosts);

    [XmlRpcMethod("metaWeblog.newPost",
      Description="Makes a new post to a designated blog using the "
      + "metaWeblog API. Returns postid as a string.")]
    string newPost(
      string blogid,
      string username,
      string password,
      Post post,
      bool publish);

      [XmlRpcMethod("metaWeblog.newMediaObject",
        Description = "Uploads an image, movie, song, or other media "
        + "using the metaWeblog API. Returns the metaObject struct.")]
      mediaObjectInfo newMediaObject(object blogid, string username, string password, mediaObject mediaobject);

	  #region BloggerAPI Members


	  [XmlRpcMethod("blogger.deletePost",
		   Description="Deletes a post.")]
	  [return: XmlRpcReturnValue(Description="Always returns true.")]
	  bool deletePost(
		  string appKey,
		  string postid,
		  string username,
		  string password,
		  [XmlRpcParameter(
			   Description="Where applicable, this specifies whether the blog "
			   + "should be republished after the post has been deleted.")]
		  bool publish);

	  [XmlRpcMethod("blogger.getUsersBlogs",
		   Description="Returns information on all the blogs a given user "
		   + "is a member.")]
	  BlogInfo[] getUsersBlogs(
		  string appKey,
		  string username,
		  string password);

	  #endregion
  }
}
