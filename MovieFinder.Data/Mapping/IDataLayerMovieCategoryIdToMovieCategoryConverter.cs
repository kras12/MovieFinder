using AutoMapper;
using MovieFinder.Data.Models;

namespace MovieFinder.Data.Mapping;

/// <summary>
/// Interface for a type converter class that converts category IDs of type <see cref="int"/> to <see cref="MovieCategory"/> instances. 
/// It supports both single categories as well as collection of categories. 
/// </summary>
internal interface IDataLayerMovieCategoryIdToMovieCategoryConverter 
    : IValueConverter<List<int>, List<MovieCategory>>, IValueConverter<int, MovieCategory>
{

}