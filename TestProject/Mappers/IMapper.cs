namespace TestProject.Mappers
{
    public interface IMapper<TWeb, TBl>
    {
        TWeb ToWebModel(TBl model);
        TBl ToBlModel(TWeb model);
    }
}
