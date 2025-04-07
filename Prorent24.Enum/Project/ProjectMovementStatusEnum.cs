using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Enum.Project
{
    public enum ProjectMovementStatusEnum
    {
        Pack = 1, //Упаковать
        Prepared = 2, //Собрано
        Transportation = 3, //Транспортировка
        OnMounting = 4, //На монтаже
        InWork = 5, //В работе
        Dismantling = 6, //Демонтаж
        Return = 7 //Возврат
    }
}
