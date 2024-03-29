﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public enum MilitaryRank
    {
        一级上将 = 1,//（设而未授，根据1994年5月12日第八届全国人大常委会第七次会议通过的决定取消[10]）；
        上将 = 2,//（中央军事委员会副主席、委员，战区级正职）； 三颗金星
        中将 = 3,//（战区级正职，战区级副职，正军职）； 二颗金星
        少将 = 4,//（战区级副职，正军职，副军职，正师职）；一颗金星
        校官 = 5,//（两杠）[8]

        大校 = 6,//（副军职，正师职，副师职／正旅职）；两杠四星
        上校 = 7,//（副师职／正旅职，正团职／副旅职）； 两杠三星
        中校 = 8,//（正团职／副旅职，副团职，正营职）； 两杠二星
        少校 = 9,//（副团职，正营职，副营职）；两杠一星
        尉官 = 10,//（一杠）[8]

        上尉 =11,//（副营职，正连职，副连职）； 一杠三星
        中尉 = 12,//（正连职，副连职，排职）；一杠二星
        少尉 = 13,//（排职）；一杠一星
        学员 = 14,//（排职待遇）；空白肩章，后改为一杠无星[8]
    }
}
