namespace TestProject.BL.Mappers
{
    public interface IMapper<TBl, TDal>
    {
        TBl ToBlModel(TDal model);
        TDal ToDalModel(TBl model);
    }
}
