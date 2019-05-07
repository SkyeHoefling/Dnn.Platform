using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetNuke.Library.Contracts.Entities.Tabs
{
    public interface ITabUrlInfo
    {
        string CultureCode { get; set; }
        string HttpStatus { get; set; }
        bool IsSystem { get; set; }
        int PortalAliasId { get; set; }
        PortalAliasUsageType PortalAliasUsage { get; set; }
        string QueryString { get; set; }
        int SeqNum { get; set; }
        int TabId { get; set; }
        int? CreatedByUserId { get; set; }
        int? LastModifiedByUserId { get; set; }

        string Url { get; set; }
    }
}
