using System.ComponentModel;

namespace HITSBackEnd.Models.AddressModels
{
    public enum AddressLevel
    {
        [Description("Субъект РФ")] Region,
        [Description("Административный район")] AdministrativeArea,
        [Description("Муниципальный район")] MunicipalArea,
        [Description("Сельское городское поселение")] RuralUrbanSettlement,
        [Description("Город")] City,
        [Description("Населенный пункт")] Locality,
        [Description("Элемент планировочной структуры")] ElementOfPlanningStructure,
        [Description("Элемент улично-дорожной сети")] ElementOfRoadNetwork,
        [Description("Земля")] Land,
        [Description("Здание (сооружение)")] Building,
        [Description("Комната")] Room = 11,
        [Description("Жилые комнаты")] RoomInRooms,
        [Description("Автономный региональный уровень")] AutonomousRegionLevel,
        [Description("Внутригородской уровень")] IntracityLevel,
        [Description("Уровень дополнительных территорий")] AdditionalTerritoriesLevel,
        [Description("Уровень Объектов На Дополнительных Территориях")] LevelOfObjectsInAdditionalTerritories,
        [Description("Место для машины")] CarPlace
    }
}
