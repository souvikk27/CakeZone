using CakeZone.Services.Product.Services;

namespace CakeZone.Services.Product.Extension;

public static class CustomHeaderExtension
{
    public static void AddResponseHeaders(this MetaData metaData, HttpResponse response)
    {
        response.Headers.Append("X-Current-Page", metaData.CurrentPage.ToString());
        response.Headers.Append("X-Total-Pages", metaData.TotalPages.ToString());
        response.Headers.Append("X-Page-Size", metaData.PageSize.ToString());
        response.Headers.Append("X-Total-Count", metaData.TotalCount.ToString());
        response.Headers.Append("X-Has-Previous", metaData.HasPrevious.ToString());
        response.Headers.Append("X-Has-Next", metaData.HasNext.ToString());
    }
}