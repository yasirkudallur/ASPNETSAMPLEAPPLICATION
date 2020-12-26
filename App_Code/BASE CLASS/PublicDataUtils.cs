using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// Summary description for PublicDataUtils
/// </summary>
public class PublicDataUtils
{
	public PublicDataUtils()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static String GetSex(String sex)
    {
        if ("M".Equals(sex))
            return "Male";
        else if ("F".Equals(sex))
            return "Female";
        else
            if ("X".Equals(sex))
                return "Unknown";

        return "";
    }

    public static String GetOccupation(String occupation)
    {
        try
        {
            if (occupation == null)
                return "";
            return (String)occupations["" + int.Parse(occupation)];
        }
        catch
        {
            return "";
        }
    }

    public static String GetMaritalStatus(String maritalStatus)
    {
        if (maritalStatus == null)
            return "";
        String[] maritalStatuses = new String[] { "--", "اعزب", "متزوج", "مطلق", "ارمل" };

        return maritalStatuses[int.Parse(maritalStatus)];
    }

    public static String GetSponsorType(String sponsorType)
    {
        if (sponsorType == null)
            return "";
        String[] SponsorTypes = new String[] { "Parent", "Spoose", "", "", "Sheikh", "UAE Citizen", "Resident", "GCC Sponsor", "Diplomatic", "Company", "Federal Government", "Local Government", "Assimilated Government", "Heritance", "", "", "", "", "Other Sponsor type" };

        return SponsorTypes[int.Parse(sponsorType) + 3];
    }

    public static String GetResidencyType(String residencyType)
    {
        if (residencyType == null)
            return "";
        String[] ResidencyTypes = new String[] { "", "", "Work", "Resident", "Diplomatic", "", "", "Service" };

        return ResidencyTypes[int.Parse(residencyType)];
    }

    public static String RemoveCommas(String source)
    {
        if (",,".Equals(source))
            return "";
        else if (source == null)
            return "";
        return source.Replace(",", " ").Replace("  ", " ");
    }

    private static Hashtable occupations = new Hashtable();
        private static Hashtable nationalities = new Hashtable();
    static PublicDataUtils()
    {
        occupations.Add("" + (0), "Armed forces");
        occupations.Add("" + (1), "Legislators, senior officials and managers");
        occupations.Add("" + (2), "Professionals");
        occupations.Add("" + (3), "Technicians and associate professionals");
        occupations.Add("" + (4), "Clerks");
        occupations.Add("" + (5), "Service workers and shop and market sales workers");
        occupations.Add("" + (6), "Skilled agricultural and fishery workers");
        occupations.Add("" + (7), "Craft and related trades workers");
        occupations.Add("" + (8), "Plant and machine operators and assemblers");
        occupations.Add("" + (9), "Elementary occupations");
        occupations.Add("" + (10), "House Wife");
        occupations.Add("" + (11), "Student");
        occupations.Add("" + (96), "Unknown occupation");
        occupations.Add("" + (97), "Retired");
        occupations.Add("" + (98), "-");
        occupations.Add("" + (99), "Unemployed");
        occupations.Add("" + (1110), "Legislators");
        occupations.Add("" + (1120), "Senior government officials");
        occupations.Add("" + (1130), "Traditional chiefs and heads of villages");
        occupations.Add("" + (1141), "Senior officials of political-party organizations");
        occupations.Add("" + (1142), "Senior officials of employers, workers and other economic-interest organizations");
        occupations.Add("" + (1143), "Senior officials of humanitarian and other special-interest organisations");
        occupations.Add("" + (1210), "Directors and chief executives");
        occupations.Add("" + (1221), "Production and operations department managers (agr., hunting, forestry & fish.)");
        occupations.Add("" + (1222), "Production and operations department managers in manufacturing");
        occupations.Add("" + (1223), "Production and operations department managers in construction");
        occupations.Add("" + (1224), "Production and operations department managers in wholesale and retail trade");
        occupations.Add("" + (1225), "Production and operations department managers in restaurants and hotels");
        occupations.Add("" + (1226), "Production & operations department managers (transport, storage & communication)");
        occupations.Add("" + (1227), "Production and operations department managers in business services");
        occupations.Add("" + (1228), "Production & operations department managers (personal care, cleaning & services)");
        occupations.Add("" + (1229), "Production and operations department managers not elsewhere classified");
        occupations.Add("" + (1231), "Finance and administration department managers");
        occupations.Add("" + (1232), "Personnel and industrial relations department managers");
        occupations.Add("" + (1233), "Sales and marketing department managers");
        occupations.Add("" + (1234), "Advertising and public relations department managers");
        occupations.Add("" + (1235), "Supply and distribution department managers");
        occupations.Add("" + (1236), "Computing services department managers");
        occupations.Add("" + (1237), "Research and development department managers");
        occupations.Add("" + (1238), "Statistics, Economics & Planning Department Managers");
        occupations.Add("" + (1239), "Other department managers not elsewhere classified");
        occupations.Add("" + (1311), "General managers in agriculture, hunting, forestry/ and fishing");
        occupations.Add("" + (1312), "General managers in manufacturing");
        occupations.Add("" + (1313), "General managers in construction");
        occupations.Add("" + (1314), "General managers in wholesale and retail trade");
        occupations.Add("" + (1315), "General managers of restaurants and hotels");
        occupations.Add("" + (1316), "General managers in transport, storage and communications");
        occupations.Add("" + (1317), "General managers of business services");
        occupations.Add("" + (1318), "General managers in personal care, cleaning and related services");
        occupations.Add("" + (1319), "General managers not elsewhere classified");
        occupations.Add("" + (2111), "Physicists and astronomers");
        occupations.Add("" + (2112), "Meteorologists");
        occupations.Add("" + (2113), "Chemists");
        occupations.Add("" + (2114), "Geologists and geophysicists");
        occupations.Add("" + (2121), "Mathematicians and related professionals");
        occupations.Add("" + (2122), "Statisticians");
        occupations.Add("" + (2131), "Computer systems designers and analysts");
        occupations.Add("" + (2132), "Computer programmers");
        occupations.Add("" + (2139), "Computing professionals not elsewhere classified");
        occupations.Add("" + (2141), "Architects, town and traffic planners");
        occupations.Add("" + (2142), "Civil engineers");
        occupations.Add("" + (2143), "Electrical engineers");
        occupations.Add("" + (2144), "Electronics and telecommunications engineers");
        occupations.Add("" + (2145), "Mechanical engineers");
        occupations.Add("" + (2146), "Chemical engineers");
        occupations.Add("" + (2147), "Mining engineers, metallurgists and related professionals");
        occupations.Add("" + (2148), "Cartographers and surveyors");
        occupations.Add("" + (2149), "Architects, engineers and related professionals not elsewhere classified");
        occupations.Add("" + (2211), "Biologists, botanists, zoologists and related professionals");
        occupations.Add("" + (2212), "Pharmacologists, pathologists and related professionals");
        occupations.Add("" + (2213), "Agronomists and related professionals");
        occupations.Add("" + (2221), "Medical doctors");
        occupations.Add("" + (2222), "Dentists");
        occupations.Add("" + (2223), "Veterinarians");
        occupations.Add("" + (2224), "Pharmacists");
        occupations.Add("" + (2229), "Health professionals (except nursing) not elsewhere classified");
        occupations.Add("" + (2230), "Nursing and midwifery professionals");
        occupations.Add("" + (2310), "College, university and higher education teaching professionals");
        occupations.Add("" + (2320), "Secondary education teaching professionals");
        occupations.Add("" + (2331), "Primary education teaching professionals");
        occupations.Add("" + (2332), "Pre-primary education teaching professionals");
        occupations.Add("" + (2340), "Special education teaching professionals");
        occupations.Add("" + (2351), "Education methods specialists");
        occupations.Add("" + (2352), "School inspectors");
        occupations.Add("" + (2353), "Other teaching professionals not elsewhere classified");
        occupations.Add("" + (2411), "Accountants");
        occupations.Add("" + (2412), "Personnel and careers professionals");
        occupations.Add("" + (2413), "Business professionals not elsewhere classified");
        occupations.Add("" + (2421), "Lawyers");
        occupations.Add("" + (2422), "Judges");
        occupations.Add("" + (2423), "Legal professionals not elsewhere classified");
        occupations.Add("" + (2431), "Archivists and curators");
        occupations.Add("" + (2432), "Librarians and related information professionals");
        occupations.Add("" + (2441), "Economists");
        occupations.Add("" + (2442), "Sociologists, anthropologists and related professionals");
        occupations.Add("" + (2443), "Philosophers, historians and political scientists");
        occupations.Add("" + (2444), "Philologists, translators and interpreters");
        occupations.Add("" + (2445), "Psychologists");
        occupations.Add("" + (2446), "Social work professionals");
        occupations.Add("" + (2451), "Authors, journalists and other writers");
        occupations.Add("" + (2452), "Sculptors, painters and related artists");
        occupations.Add("" + (2453), "Composers, musicians and singers");
        occupations.Add("" + (2454), "Choreographers and dancers");
        occupations.Add("" + (2455), "Film, stage and related actors and directors");
        occupations.Add("" + (2460), "Religious professionals");
        occupations.Add("" + (3111), "Chemical and physical science technicians");
        occupations.Add("" + (3112), "Civil engineering technicians");
        occupations.Add("" + (3113), "Electrical engineering technicians");
        occupations.Add("" + (3114), "Electronics and telecommunications engineering technicians");
        occupations.Add("" + (3115), "Mechanical engineering technicians");
        occupations.Add("" + (3116), "Chemical engineering technicians");
        occupations.Add("" + (3117), "Mining and metallurgical technicians");
        occupations.Add("" + (3118), "Draughtspersons");
        occupations.Add("" + (3119), "Physical and engineering science technicians not elsewhere classified");
        occupations.Add("" + (3121), "Computer assistants");
        occupations.Add("" + (3122), "Computer equipment operators");
        occupations.Add("" + (3123), "Industrial robot controllers");
        occupations.Add("" + (3131), "Photographers and image and sound recording equipment operators");
        occupations.Add("" + (3132), "Broadcasting and telecommunications equipment operators");
        occupations.Add("" + (3133), "Medical equipment operators");
        occupations.Add("" + (3139), "Optical and electronic equipment operators not elsewhere classified");
        occupations.Add("" + (3141), "Ships' engineers");
        occupations.Add("" + (3142), "Ships' deck officers and pilots");
        occupations.Add("" + (3143), "Aircraft pilots and related associate professionals");
        occupations.Add("" + (3144), "Air traffic controllers");
        occupations.Add("" + (3145), "Air traffic safety technicians");
        occupations.Add("" + (3151), "Building and fire inspectors");
        occupations.Add("" + (3152), "Safety, health and quality inspectors");
        occupations.Add("" + (3211), "Life science technicians");
        occupations.Add("" + (3212), "Agronomy and forestry technicians");
        occupations.Add("" + (3213), "Farming and forestry advisers");
        occupations.Add("" + (3221), "Medical assistants");
        occupations.Add("" + (3222), "Sanitarians");
        occupations.Add("" + (3223), "Dieticians and nutritionists");
        occupations.Add("" + (3224), "Optometrists and opticians");
        occupations.Add("" + (3225), "Dental assistants");
        occupations.Add("" + (3226), "Physiotherapists and related associate professionals");
        occupations.Add("" + (3227), "Veterinary assistants");
        occupations.Add("" + (3228), "Pharmaceutical assistants");
        occupations.Add("" + (3229), "Modern health associate professionals (except nursing) not elsewhere classified");
        occupations.Add("" + (3231), "Nursing associate professionals");
        occupations.Add("" + (3232), "Midwifery associate professionals");
        occupations.Add("" + (3241), "Traditional medicine practitioners");
        occupations.Add("" + (3242), "Faith healers");
        occupations.Add("" + (3310), "Primary education teaching associate professionals");
        occupations.Add("" + (3320), "Pre-primary education teaching associate professionals");
        occupations.Add("" + (3330), "Special education teaching associate professionals");
        occupations.Add("" + (3340), "Other teaching associate professionals");
        occupations.Add("" + (3411), "Securities and finance dealers and brokers");
        occupations.Add("" + (3412), "Insurance representatives");
        occupations.Add("" + (3413), "Estate agents");
        occupations.Add("" + (3414), "Travel consultants and organisers");
        occupations.Add("" + (3415), "Technical and commercial sales representatives");
        occupations.Add("" + (3416), "Buyers");
        occupations.Add("" + (3417), "Appraisers, valuers and auctioneers");
        occupations.Add("" + (3419), "Finance and sales associate professionals not elsewhere classified");
        occupations.Add("" + (3421), "Trade brokers");
        occupations.Add("" + (3422), "Clearing and forwarding agents");
        occupations.Add("" + (3423), "Employment agents and labour contractors");
        occupations.Add("" + (3424), "Business services agents and trade brokers not elsewhere classified");
        occupations.Add("" + (3431), "Administrative secretaries and related associate professionals");
        occupations.Add("" + (3432), "Legal and related business associate professionals");
        occupations.Add("" + (3433), "Bookkeepers");
        occupations.Add("" + (3434), "Statistical, mathematical and related associate professionals");
        occupations.Add("" + (3435), "Administrative associate professionals not elsewhere classified");
        occupations.Add("" + (3441), "Customs and border inspectors");
        occupations.Add("" + (3442), "Government tax and excise officials");
        occupations.Add("" + (3443), "Government social benefits officials");
        occupations.Add("" + (3444), "Government licensing officials");
        occupations.Add("" + (3445), "Police inspectors and detectives");
        occupations.Add("" + (3449), "Customs, tax and related government associate professionals");
        occupations.Add("" + (3451), "Social work associate professionals");
        occupations.Add("" + (3461), "Decorators and commercial designers");
        occupations.Add("" + (3462), "Radio, television and other announcers");
        occupations.Add("" + (3463), "Street, night-club and related musicians, singers and dancers");
        occupations.Add("" + (3464), "Clowns, magicians, acrobats and related associate professionals");
        occupations.Add("" + (3465), "Athletes, sportspersons and related associate professionals");
        occupations.Add("" + (3470), "Religious associate professionals");
        occupations.Add("" + (4111), "Stenographers and typists");
        occupations.Add("" + (4112), "Word-processor and related operators");
        occupations.Add("" + (4113), "Data entry operators");
        occupations.Add("" + (4114), "Calculating-machine operators");
        occupations.Add("" + (4115), "Secretaries");
        occupations.Add("" + (4121), "Accounting and bookkeeping clerks");
        occupations.Add("" + (4122), "Statistical and finance clerks");
        occupations.Add("" + (4131), "Stock clerks");
        occupations.Add("" + (4132), "Production clerks");
        occupations.Add("" + (4133), "Transport clerks");
        occupations.Add("" + (4141), "Library and filing clerks");
        occupations.Add("" + (4142), "Mail carriers and sorting clerks");
        occupations.Add("" + (4143), "Coding, proof-reading and related clerks");
        occupations.Add("" + (4144), "Scribes and related workers");
        occupations.Add("" + (4190), "Other office clerks");
        occupations.Add("" + (4211), "Cashiers and ticket clerks");
        occupations.Add("" + (4212), "Tellers and other counter clerks");
        occupations.Add("" + (4213), "Bookmakers and croupiers");
        occupations.Add("" + (4214), "Pawnbrokers and money-lenders");
        occupations.Add("" + (4215), "Debt-collectors and related workers");
        occupations.Add("" + (4221), "Travel agency and related clerks");
        occupations.Add("" + (4222), "Receptionists and information clerks");
        occupations.Add("" + (4223), "Telephone switchboard operators");
        occupations.Add("" + (5111), "Travel attendants and travel stewards");
        occupations.Add("" + (5112), "Transport conductors");
        occupations.Add("" + (5113), "Travel guides");
        occupations.Add("" + (5121), "Housekeepers and related workers");
        occupations.Add("" + (5122), "Cooks");
        occupations.Add("" + (5123), "Waiters, waitresses and bartenders");
        occupations.Add("" + (5131), "Child-care workers");
        occupations.Add("" + (5132), "Institution-based personal care workers");
        occupations.Add("" + (5133), "Home-based personal care workers");
        occupations.Add("" + (5134), "Personal care and related workers not elsewhere classified");
        occupations.Add("" + (5141), "Hairdressers, barbers, beauticians and related workers");
        occupations.Add("" + (5142), "Companions and valets");
        occupations.Add("" + (5143), "Undertakers and embalmers");
        occupations.Add("" + (5144), "Other personal services workers not elsewhere classified");
        occupations.Add("" + (5151), "Astrologers and related workers");
        occupations.Add("" + (5152), "Fortune-tellers, palmists and related workers");
        occupations.Add("" + (5161), "Fire-fighters");
        occupations.Add("" + (5162), "Police officers");
        occupations.Add("" + (5163), "Prison guards");
        occupations.Add("" + (5164), "Protective services workers not elsewhere classified");
        occupations.Add("" + (5210), "Fashion and other models");
        occupations.Add("" + (5220), "Shop salespersons and demonstrators");
        occupations.Add("" + (5230), "Stall and market salespersons");
        occupations.Add("" + (6111), "Field crop and vegetable growers");
        occupations.Add("" + (6112), "Tree and shrub crop growers");
        occupations.Add("" + (6113), "Gardeners, horticultural and nursery growers");
        occupations.Add("" + (6114), "Mixed-crop growers");
        occupations.Add("" + (6121), "Dairy and livestock producers");
        occupations.Add("" + (6122), "Poultry producers");
        occupations.Add("" + (6123), "Apiarists and sericulturists");
        occupations.Add("" + (6124), "Mixed-animal producers");
        occupations.Add("" + (6129), "Market-oriented animal producers and related workers not elsewhere classified");
        occupations.Add("" + (6130), "Market-oriented crop and animal producers");
        occupations.Add("" + (6141), "Forestry workers and loggers");
        occupations.Add("" + (6142), "Charcoal burners and related workers");
        occupations.Add("" + (6151), "Aquatic-life cultivation workers");
        occupations.Add("" + (6152), "Inland and coastal waters fishery workers");
        occupations.Add("" + (6153), "Deep-sea fishery workers");
        occupations.Add("" + (6154), "Hunters and trappers");
        occupations.Add("" + (6210), "Subsistence agricultural and fishery workers");
        occupations.Add("" + (7111), "Miners and quarry workers");
        occupations.Add("" + (7112), "Shotfirers and blasters");
        occupations.Add("" + (7113), "Stone splitters, cutters and carvers");
        occupations.Add("" + (7121), "Builders, traditional materials");
        occupations.Add("" + (7122), "Bricklayers and stonemasons");
        occupations.Add("" + (7123), "Concrete placers, concrete finishers and related workers");
        occupations.Add("" + (7124), "Carpenters and joiners");
        occupations.Add("" + (7125), "Building frame and related trades workers not elsewhere classified");
        occupations.Add("" + (7131), "Roofers");
        occupations.Add("" + (7132), "Floor layers and tile setters");
        occupations.Add("" + (7133), "Plasterers");
        occupations.Add("" + (7134), "Insulation workers");
        occupations.Add("" + (7135), "Glaziers");
        occupations.Add("" + (7136), "Building and related electricians");
        occupations.Add("" + (7137), "Plumbers and pipe fitters");
        occupations.Add("" + (7141), "Painters and related workers");
        occupations.Add("" + (7142), "Varnishers and related painters");
        occupations.Add("" + (7143), "Tiles & Wooden Plats fitters");
        occupations.Add("" + (7144), "Building structure cleaners");
        occupations.Add("" + (7211), "Metal moulders and coremakers");
        occupations.Add("" + (7212), "Welders and flamecutters");
        occupations.Add("" + (7213), "Sheet metal workers");
        occupations.Add("" + (7214), "Structural-metal preparers and erectors");
        occupations.Add("" + (7215), "Riggers and cable splicers");
        occupations.Add("" + (7216), "Underwater workers");
        occupations.Add("" + (7221), "Blacksmiths, hammer-smiths and forging-press workers");
        occupations.Add("" + (7222), "Tool-makers and related workers");
        occupations.Add("" + (7223), "Machine-tool setters and setter-operators");
        occupations.Add("" + (7224), "Metal wheel-grinders, polishers and tool sharpeners");
        occupations.Add("" + (7231), "Motor vehicle mechanics and fitters");
        occupations.Add("" + (7232), "Aircraft engine mechanics and fitters");
        occupations.Add("" + (7239), "Agricultural- or industrial-machinery mechanics and fitters");
        occupations.Add("" + (7241), "Electrical mechanics and fitters");
        occupations.Add("" + (7242), "Electronics fitters");
        occupations.Add("" + (7243), "Electronics mechanics and servicers");
        occupations.Add("" + (7244), "Telegraph and telephone installers and servicers");
        occupations.Add("" + (7245), "Electrical line installers, repairers and cable jointers");
        occupations.Add("" + (7311), "Precision-instrument makers and repairers");
        occupations.Add("" + (7312), "Musical instrument makers and tuners");
        occupations.Add("" + (7313), "Jewellery and precious-metal workers");
        occupations.Add("" + (7321), "Abrasive wheel formers, potters and related workers");
        occupations.Add("" + (7322), "Glass makers, cutters, grinders and finishers");
        occupations.Add("" + (7323), "Glass engravers and etchers");
        occupations.Add("" + (7324), "Glass, ceramics and related decorative painters");
        occupations.Add("" + (7331), "Handicraft workers in wood and related materials");
        occupations.Add("" + (7332), "Handicraft workers in textile, leather and related materials");
        occupations.Add("" + (7341), "Compositors, typesetters and related workers");
        occupations.Add("" + (7342), "Stereotypers and electrotypers");
        occupations.Add("" + (7343), "Printing engravers and etchers");
        occupations.Add("" + (7344), "Photographic and related workers");
        occupations.Add("" + (7345), "Bookbinders and related workers");
        occupations.Add("" + (7346), "Silk-screen, block and textile printers");
        occupations.Add("" + (7411), "Butchers, fishmongers and related food preparers");
        occupations.Add("" + (7412), "Bakers, pastry-cooks and confectionery makers");
        occupations.Add("" + (7413), "Dairy-products makers");
        occupations.Add("" + (7414), "Fruit, vegetable and related preservers");
        occupations.Add("" + (7415), "Food and beverage tasters and graders");
        occupations.Add("" + (7416), "Tobacco preparers and tobacco products makers");
        occupations.Add("" + (7421), "Wood treaters");
        occupations.Add("" + (7422), "Cabinet makers and related workers");
        occupations.Add("" + (7423), "Woodworking machine setters and setter-operators");
        occupations.Add("" + (7424), "Basketry weavers, brush makers and related workers");
        occupations.Add("" + (7431), "Fibre preparers");
        occupations.Add("" + (7432), "Weavers, knitters and related workers");
        occupations.Add("" + (7433), "Tailors, dressmakers and hatters");
        occupations.Add("" + (7434), "Furriers and related workers");
        occupations.Add("" + (7435), "Textile, leather and related pattern-makers and cutters");
        occupations.Add("" + (7436), "Sewers, embroiderers and related workers");
        occupations.Add("" + (7437), "Upholsterers and related workers");
        occupations.Add("" + (7439), "Textile and dress-make workers");
        occupations.Add("" + (7441), "Pelt dressers, tanners and fellmongers");
        occupations.Add("" + (7442), "Shoe-makers and related workers");
        occupations.Add("" + (8111), "Mining-plant operators");
        occupations.Add("" + (8112), "Mineral-ore- and stone-processing-plant operators");
        occupations.Add("" + (8113), "Well drillers and borers and related workers");
        occupations.Add("" + (8121), "Ore and metal furnace operators");
        occupations.Add("" + (8122), "Metal melters, casters and rolling-mill operators");
        occupations.Add("" + (8123), "Metal-heat-treating-plant operators");
        occupations.Add("" + (8124), "Metal drawers and extruders");
        occupations.Add("" + (8131), "Glass and ceramics kiln and related machine operators");
        occupations.Add("" + (8132), "Glass, ceramics and related plant operators");
        occupations.Add("" + (8141), "Wood-processing-plant operators");
        occupations.Add("" + (8142), "Paper-pulp plant operators");
        occupations.Add("" + (8143), "Papermaking-plant operators");
        occupations.Add("" + (8151), "Crushing-, grinding- and chemical-mixing-machinery operators");
        occupations.Add("" + (8152), "Chemical-heat-treating-plant operators");
        occupations.Add("" + (8153), "Chemical-filtering- and separating-equipment operators");
        occupations.Add("" + (8154), "Chemical-still and reactor operators (except petroleum and natural gas)");
        occupations.Add("" + (8155), "Petroleum- and natural-gas-refining-plant operators");
        occupations.Add("" + (8159), "Chemical-processing-plant operators not elsewhere classified");
        occupations.Add("" + (8161), "Power-production plant operators");
        occupations.Add("" + (8162), "Steam-engine and boiler operators");
        occupations.Add("" + (8163), "Incinerator, water-treatment and related plant operators");
        occupations.Add("" + (8171), "Automated-assembly-line operators");
        occupations.Add("" + (8172), "Industrial-robot operators");
        occupations.Add("" + (8211), "Machine-tool operators");
        occupations.Add("" + (8212), "Cement and other mineral products machine operators");
        occupations.Add("" + (8221), "Pharmaceutical- and toiletry-products machine operators");
        occupations.Add("" + (8222), "Ammunition- and explosive-products machine operators");
        occupations.Add("" + (8223), "Metal finishing-, plating- and coating-machine operators");
        occupations.Add("" + (8224), "Photographic-products machine operators");
        occupations.Add("" + (8229), "Chemical-products machine operators not elsewhere classified");
        occupations.Add("" + (8231), "Rubber-products machine operators");
        occupations.Add("" + (8232), "Plastic-products machine operators");
        occupations.Add("" + (8241), "Wood-products machine operators");
        occupations.Add("" + (8251), "Printing-machine operators");
        occupations.Add("" + (8252), "Bookbinding-machine operators");
        occupations.Add("" + (8253), "Paper-products machine operators");
        occupations.Add("" + (8261), "Fibre-preparing-, spinning- and winding-machine operators");
        occupations.Add("" + (8262), "Weaving- and knitting-machine operators");
        occupations.Add("" + (8263), "Sewing-machine operators");
        occupations.Add("" + (8264), "Bleaching-, dyeing- and cleaning-machine operators");
        occupations.Add("" + (8265), "Fur- and leather-preparing-machine operators");
        occupations.Add("" + (8266), "Shoemaking- and related machine operators");
        occupations.Add("" + (8269), "Textile-, fur- and leather-products machine operators not elsewhere classified");
        occupations.Add("" + (8271), "Meat- and fish-processing-machine operators");
        occupations.Add("" + (8272), "Dairy-products machine operators");
        occupations.Add("" + (8273), "Grain- and spice-milling-machine operators");
        occupations.Add("" + (8274), "Baked-goods, cereal and chocolate-products machine operators");
        occupations.Add("" + (8275), "Fruit-, vegetable- and nut-processing-machine operators");
        occupations.Add("" + (8276), "Sugar production machine operators");
        occupations.Add("" + (8277), "Tea-, coffee-, and cocoa-processing-machine operators");
        occupations.Add("" + (8278), "Brewers, wine and other beverage machine operators");
        occupations.Add("" + (8279), "Tobacco production machine operators");
        occupations.Add("" + (8281), "Mechanical-machinery assemblers");
        occupations.Add("" + (8282), "Electrical-equipment assemblers");
        occupations.Add("" + (8283), "Electronic-equipment assemblers");
        occupations.Add("" + (8284), "Metal-, rubber- and plastic-products assemblers");
        occupations.Add("" + (8285), "Wood and related products assemblers");
        occupations.Add("" + (8286), "Paperboard, textile and related products assemblers");
        occupations.Add("" + (8290), "Other machine operators and assemblers");
        occupations.Add("" + (8311), "Locomotive-engine drivers");
        occupations.Add("" + (8312), "Railway brakers, signallers and shunters");
        occupations.Add("" + (8321), "Motor-cycle drivers");
        occupations.Add("" + (8322), "Car, taxi and van drivers");
        occupations.Add("" + (8323), "Bus and tram drivers");
        occupations.Add("" + (8324), "Heavy-truck and lorry drivers");
        occupations.Add("" + (8331), "Motorised farm and forestry plant operators");
        occupations.Add("" + (8332), "Earth-moving- and related plant operators");
        occupations.Add("" + (8333), "Crane, hoist and related plant operators");
        occupations.Add("" + (8334), "Lifting-truck operators");
        occupations.Add("" + (8340), "Ships' deck crews and related workers");
        occupations.Add("" + (9111), "Street food vendors");
        occupations.Add("" + (9112), "Street vendors, non-food products");
        occupations.Add("" + (9113), "Door-to-door and telephone salespersons");
        occupations.Add("" + (9121), "Shoe cleaning and other street services elementary occupations");
        occupations.Add("" + (9131), "Domestic helpers and cleaners");
        occupations.Add("" + (9132), "Helpers and cleaners in offices, hotels and other establishments");
        occupations.Add("" + (9133), "Hand-launderers and pressers");
        occupations.Add("" + (9141), "Building caretakers");
        occupations.Add("" + (9142), "Vehicle, window and related cleaners");
        occupations.Add("" + (9151), "Messengers, package and luggage porters and deliverers");
        occupations.Add("" + (9152), "Doorkeepers, watchpersons and related workers");
        occupations.Add("" + (9153), "Vending-machine money collectors, meter readers and related workers");
        occupations.Add("" + (9161), "Garbage collectors");
        occupations.Add("" + (9162), "Sweepers and related labourers");
        occupations.Add("" + (9211), "Farm-hands and labourers");
        occupations.Add("" + (9212), "Forestry labourers");
        occupations.Add("" + (9213), "Fishery, hunting and trapping labourers");
        occupations.Add("" + (9311), "Mining and quarrying labourers");
        occupations.Add("" + (9312), "Construction and maintenance labourers: roads, dams and similar constructions");
        occupations.Add("" + (9313), "Building construction labourers");
        occupations.Add("" + (9321), "Assembling labourers");
        occupations.Add("" + (9322), "Hand packers and other manufacturing labourers");
        occupations.Add("" + (9331), "Hand or pedal vehicle drivers");
        occupations.Add("" + (9332), "Drivers of animal-drawn vehicles and machinery");
        occupations.Add("" + (9333), "Freight handlers");
    }
 
   

}