using System.ComponentModel;

namespace HITSBackEnd.Models.AddressModels
{
    public enum HousesLevel
    {
        [Description("Владение")] Ownership,
        [Description("Дом")] House,
        [Description("Домовладение")] Household,
        [Description("Гараж")] Garage,
        [Description("Здание")] Building,
        [Description("Шахта")] Mine,
        [Description("Строение")] Structure,
        [Description("Сооружение")] Facility,
        [Description("Литера")] Letter,
        [Description("Корпус")] Corpus,
        [Description("Подвал")] Basement,
        [Description("Котельная")] BoilerRoom,
        [Description("Погреб")] Cellar,
        [Description("Объект незавершенного строительства")] ConstructionInProgress
    }
}
