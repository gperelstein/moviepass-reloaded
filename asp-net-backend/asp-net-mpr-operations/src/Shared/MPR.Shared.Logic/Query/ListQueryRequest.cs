using MediatR;
using MPR.Shared.Domain.Abstractions;
using MPR.Shared.Logic.Responses;
using System.ComponentModel.DataAnnotations;

namespace MPR.Shared.Logic.Query
{
    public class ListQueryRequest<TResponse> : IRequest<Response<TResponse>>
    {
        public ListQueryRequest()
        {
            SortingName = nameof(ISortable.LastUpdatedAt);
            SortType = SortingType.Descending;
        }

        [Display(Name = "Sort Type")]
        public string SortType { get; set; }

        [Display(Name = "Sorting Property Name")]
        public string SortingName { get; set; }
    }
}
