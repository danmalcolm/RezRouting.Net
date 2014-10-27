using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace RezRouting.Tests.AspNetMvc.Benchmarks.Controllers 
{
    public class DemoData 
	{
		public static IEnumerable<Tuple<string,Type>> Resources 
		{
			get
			{
				yield return Tuple.Create("Abyssinians", typeof(AbyssiniansController));		
				yield return Tuple.Create("Accelerators", typeof(AcceleratorsController));		
				yield return Tuple.Create("Accordions", typeof(AccordionsController));		
				yield return Tuple.Create("Accounts", typeof(AccountsController));		
				yield return Tuple.Create("Baboons", typeof(BaboonsController));		
				yield return Tuple.Create("Backbones", typeof(BackbonesController));		
				yield return Tuple.Create("Badges", typeof(BadgesController));		
				yield return Tuple.Create("Badgers", typeof(BadgersController));		
				yield return Tuple.Create("Cabbages", typeof(CabbagesController));		
				yield return Tuple.Create("Cabinets", typeof(CabinetsController));		
				yield return Tuple.Create("Cakes", typeof(CakesController));		
				yield return Tuple.Create("Calculators", typeof(CalculatorsController));		
				yield return Tuple.Create("Daffodils", typeof(DaffodilsController));		
				yield return Tuple.Create("Daisies", typeof(DaisiesController));		
				yield return Tuple.Create("Dancers", typeof(DancersController));		
				yield return Tuple.Create("Dashboards", typeof(DashboardsController));		
				yield return Tuple.Create("Eagles", typeof(EaglesController));		
				yield return Tuple.Create("Ears", typeof(EarsController));		
				yield return Tuple.Create("Earthquakes", typeof(EarthquakesController));		
				yield return Tuple.Create("Elephants", typeof(ElephantsController));		
				yield return Tuple.Create("Faces", typeof(FacesController));		
				yield return Tuple.Create("Facilities", typeof(FacilitiesController));		
				yield return Tuple.Create("Facts", typeof(FactsController));		
				yield return Tuple.Create("Factories", typeof(FactoriesController));		
				yield return Tuple.Create("Garages", typeof(GaragesController));		
				yield return Tuple.Create("Gardens", typeof(GardensController));		
				yield return Tuple.Create("Ghosts", typeof(GhostsController));		
				yield return Tuple.Create("Giraffes", typeof(GiraffesController));		
				yield return Tuple.Create("Halibuts", typeof(HalibutsController));		
				yield return Tuple.Create("Hamburgers", typeof(HamburgersController));		
				yield return Tuple.Create("Hammers", typeof(HammersController));		
				yield return Tuple.Create("Hamsters", typeof(HamstersController));		
				yield return Tuple.Create("Icicles", typeof(IciclesController));		
				yield return Tuple.Create("Insects", typeof(InsectsController));		
				yield return Tuple.Create("Invoices", typeof(InvoicesController));		
				yield return Tuple.Create("Jaguars", typeof(JaguarsController));		
				yield return Tuple.Create("Jeeps", typeof(JeepsController));		
				yield return Tuple.Create("Jewels", typeof(JewelsController));		
				yield return Tuple.Create("Jokes", typeof(JokesController));		
				yield return Tuple.Create("Kangaroos", typeof(KangaroosController));		
				yield return Tuple.Create("Kayaks", typeof(KayaksController));		
				yield return Tuple.Create("Kettles", typeof(KettlesController));		
				yield return Tuple.Create("Kittens", typeof(KittensController));		
				yield return Tuple.Create("Languages", typeof(LanguagesController));		
				yield return Tuple.Create("Larches", typeof(LarchesController));		
				yield return Tuple.Create("Lawyers", typeof(LawyersController));		
				yield return Tuple.Create("Lobsters", typeof(LobstersController));		
				yield return Tuple.Create("Machines", typeof(MachinesController));		
				yield return Tuple.Create("Magazines", typeof(MagazinesController));		
				yield return Tuple.Create("Managers", typeof(ManagersController));		
				yield return Tuple.Create("Mandolins", typeof(MandolinsController));		
				yield return Tuple.Create("Napkins", typeof(NapkinsController));		
				yield return Tuple.Create("Needles", typeof(NeedlesController));		
				yield return Tuple.Create("Noodles", typeof(NoodlesController));		
				yield return Tuple.Create("Nuts", typeof(NutsController));		
				yield return Tuple.Create("Occupations", typeof(OccupationsController));		
				yield return Tuple.Create("Offences", typeof(OffencesController));		
				yield return Tuple.Create("Offices", typeof(OfficesController));		
				yield return Tuple.Create("Operas", typeof(OperasController));		
				yield return Tuple.Create("Packages", typeof(PackagesController));		
				yield return Tuple.Create("Packets", typeof(PacketsController));		
				yield return Tuple.Create("Pancakes", typeof(PancakesController));		
				yield return Tuple.Create("Papers", typeof(PapersController));		
				yield return Tuple.Create("Queens", typeof(QueensController));		
				yield return Tuple.Create("Questions", typeof(QuestionsController));		
				yield return Tuple.Create("Quilts", typeof(QuiltsController));		
				yield return Tuple.Create("Quinces", typeof(QuincesController));		
				yield return Tuple.Create("Rabbits", typeof(RabbitsController));		
				yield return Tuple.Create("Radiators", typeof(RadiatorsController));		
				yield return Tuple.Create("Radishes", typeof(RadishesController));		
				yield return Tuple.Create("Rainbows", typeof(RainbowsController));		
				yield return Tuple.Create("Sailors", typeof(SailorsController));		
				yield return Tuple.Create("Salamanders", typeof(SalamandersController));		
				yield return Tuple.Create("Sardines", typeof(SardinesController));		
				yield return Tuple.Create("Sausages", typeof(SausagesController));		
				yield return Tuple.Create("Tables", typeof(TablesController));		
				yield return Tuple.Create("Tadpoles", typeof(TadpolesController));		
				yield return Tuple.Create("Taxis", typeof(TaxisController));		
				yield return Tuple.Create("Teachers", typeof(TeachersController));		
				yield return Tuple.Create("Umbrellas", typeof(UmbrellasController));		
				yield return Tuple.Create("Uncles", typeof(UnclesController));		
				yield return Tuple.Create("Underpants", typeof(UnderpantsController));		
				yield return Tuple.Create("Units", typeof(UnitsController));		
				yield return Tuple.Create("Valleys", typeof(ValleysController));		
				yield return Tuple.Create("Values", typeof(ValuesController));		
				yield return Tuple.Create("Vans", typeof(VansController));		
				yield return Tuple.Create("Wallets", typeof(WalletsController));		
				yield return Tuple.Create("Waterfalls", typeof(WaterfallsController));		
				yield return Tuple.Create("Waves", typeof(WavesController));		
				yield return Tuple.Create("Weeds", typeof(WeedsController));		
				yield return Tuple.Create("Xylophones", typeof(XylophonesController));		
				yield return Tuple.Create("Yachts", typeof(YachtsController));		
				yield return Tuple.Create("Yaks", typeof(YaksController));		
				yield return Tuple.Create("Yards", typeof(YardsController));		
				yield return Tuple.Create("Years", typeof(YearsController));		
				yield return Tuple.Create("Yams", typeof(YamsController));		
				yield return Tuple.Create("Zebras", typeof(ZebrasController));		
				yield return Tuple.Create("Zephyrs", typeof(ZephyrsController));		
				yield return Tuple.Create("Zippers", typeof(ZippersController));		
				yield return Tuple.Create("Zoos", typeof(ZoosController));		
				
			}
		}

	}
	


	public class AbyssiniansController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Abyssinians.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Abyssinians.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Abyssinians.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Abyssinians.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Abyssinians.Action5");
		}

	}

	public class AcceleratorsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Accelerators.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Accelerators.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Accelerators.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Accelerators.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Accelerators.Action5");
		}

	}

	public class AccordionsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Accordions.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Accordions.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Accordions.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Accordions.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Accordions.Action5");
		}

	}

	public class AccountsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Accounts.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Accounts.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Accounts.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Accounts.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Accounts.Action5");
		}

	}

	public class BaboonsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Baboons.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Baboons.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Baboons.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Baboons.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Baboons.Action5");
		}

	}

	public class BackbonesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Backbones.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Backbones.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Backbones.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Backbones.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Backbones.Action5");
		}

	}

	public class BadgesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Badges.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Badges.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Badges.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Badges.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Badges.Action5");
		}

	}

	public class BadgersController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Badgers.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Badgers.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Badgers.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Badgers.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Badgers.Action5");
		}

	}

	public class CabbagesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Cabbages.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Cabbages.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Cabbages.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Cabbages.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Cabbages.Action5");
		}

	}

	public class CabinetsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Cabinets.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Cabinets.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Cabinets.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Cabinets.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Cabinets.Action5");
		}

	}

	public class CakesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Cakes.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Cakes.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Cakes.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Cakes.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Cakes.Action5");
		}

	}

	public class CalculatorsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Calculators.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Calculators.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Calculators.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Calculators.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Calculators.Action5");
		}

	}

	public class DaffodilsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Daffodils.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Daffodils.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Daffodils.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Daffodils.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Daffodils.Action5");
		}

	}

	public class DaisiesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Daisies.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Daisies.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Daisies.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Daisies.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Daisies.Action5");
		}

	}

	public class DancersController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Dancers.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Dancers.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Dancers.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Dancers.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Dancers.Action5");
		}

	}

	public class DashboardsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Dashboards.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Dashboards.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Dashboards.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Dashboards.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Dashboards.Action5");
		}

	}

	public class EaglesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Eagles.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Eagles.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Eagles.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Eagles.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Eagles.Action5");
		}

	}

	public class EarsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Ears.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Ears.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Ears.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Ears.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Ears.Action5");
		}

	}

	public class EarthquakesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Earthquakes.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Earthquakes.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Earthquakes.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Earthquakes.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Earthquakes.Action5");
		}

	}

	public class ElephantsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Elephants.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Elephants.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Elephants.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Elephants.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Elephants.Action5");
		}

	}

	public class FacesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Faces.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Faces.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Faces.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Faces.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Faces.Action5");
		}

	}

	public class FacilitiesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Facilities.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Facilities.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Facilities.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Facilities.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Facilities.Action5");
		}

	}

	public class FactsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Facts.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Facts.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Facts.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Facts.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Facts.Action5");
		}

	}

	public class FactoriesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Factories.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Factories.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Factories.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Factories.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Factories.Action5");
		}

	}

	public class GaragesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Garages.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Garages.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Garages.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Garages.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Garages.Action5");
		}

	}

	public class GardensController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Gardens.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Gardens.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Gardens.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Gardens.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Gardens.Action5");
		}

	}

	public class GhostsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Ghosts.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Ghosts.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Ghosts.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Ghosts.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Ghosts.Action5");
		}

	}

	public class GiraffesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Giraffes.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Giraffes.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Giraffes.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Giraffes.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Giraffes.Action5");
		}

	}

	public class HalibutsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Halibuts.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Halibuts.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Halibuts.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Halibuts.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Halibuts.Action5");
		}

	}

	public class HamburgersController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Hamburgers.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Hamburgers.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Hamburgers.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Hamburgers.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Hamburgers.Action5");
		}

	}

	public class HammersController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Hammers.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Hammers.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Hammers.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Hammers.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Hammers.Action5");
		}

	}

	public class HamstersController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Hamsters.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Hamsters.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Hamsters.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Hamsters.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Hamsters.Action5");
		}

	}

	public class IciclesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Icicles.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Icicles.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Icicles.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Icicles.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Icicles.Action5");
		}

	}

	public class InsectsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Insects.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Insects.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Insects.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Insects.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Insects.Action5");
		}

	}

	public class InvoicesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Invoices.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Invoices.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Invoices.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Invoices.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Invoices.Action5");
		}

	}

	public class JaguarsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Jaguars.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Jaguars.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Jaguars.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Jaguars.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Jaguars.Action5");
		}

	}

	public class JeepsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Jeeps.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Jeeps.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Jeeps.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Jeeps.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Jeeps.Action5");
		}

	}

	public class JewelsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Jewels.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Jewels.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Jewels.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Jewels.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Jewels.Action5");
		}

	}

	public class JokesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Jokes.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Jokes.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Jokes.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Jokes.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Jokes.Action5");
		}

	}

	public class KangaroosController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Kangaroos.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Kangaroos.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Kangaroos.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Kangaroos.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Kangaroos.Action5");
		}

	}

	public class KayaksController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Kayaks.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Kayaks.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Kayaks.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Kayaks.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Kayaks.Action5");
		}

	}

	public class KettlesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Kettles.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Kettles.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Kettles.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Kettles.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Kettles.Action5");
		}

	}

	public class KittensController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Kittens.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Kittens.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Kittens.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Kittens.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Kittens.Action5");
		}

	}

	public class LanguagesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Languages.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Languages.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Languages.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Languages.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Languages.Action5");
		}

	}

	public class LarchesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Larches.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Larches.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Larches.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Larches.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Larches.Action5");
		}

	}

	public class LawyersController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Lawyers.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Lawyers.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Lawyers.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Lawyers.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Lawyers.Action5");
		}

	}

	public class LobstersController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Lobsters.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Lobsters.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Lobsters.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Lobsters.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Lobsters.Action5");
		}

	}

	public class MachinesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Machines.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Machines.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Machines.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Machines.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Machines.Action5");
		}

	}

	public class MagazinesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Magazines.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Magazines.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Magazines.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Magazines.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Magazines.Action5");
		}

	}

	public class ManagersController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Managers.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Managers.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Managers.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Managers.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Managers.Action5");
		}

	}

	public class MandolinsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Mandolins.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Mandolins.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Mandolins.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Mandolins.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Mandolins.Action5");
		}

	}

	public class NapkinsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Napkins.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Napkins.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Napkins.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Napkins.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Napkins.Action5");
		}

	}

	public class NeedlesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Needles.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Needles.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Needles.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Needles.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Needles.Action5");
		}

	}

	public class NoodlesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Noodles.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Noodles.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Noodles.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Noodles.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Noodles.Action5");
		}

	}

	public class NutsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Nuts.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Nuts.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Nuts.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Nuts.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Nuts.Action5");
		}

	}

	public class OccupationsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Occupations.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Occupations.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Occupations.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Occupations.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Occupations.Action5");
		}

	}

	public class OffencesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Offences.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Offences.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Offences.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Offences.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Offences.Action5");
		}

	}

	public class OfficesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Offices.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Offices.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Offices.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Offices.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Offices.Action5");
		}

	}

	public class OperasController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Operas.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Operas.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Operas.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Operas.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Operas.Action5");
		}

	}

	public class PackagesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Packages.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Packages.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Packages.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Packages.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Packages.Action5");
		}

	}

	public class PacketsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Packets.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Packets.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Packets.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Packets.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Packets.Action5");
		}

	}

	public class PancakesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Pancakes.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Pancakes.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Pancakes.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Pancakes.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Pancakes.Action5");
		}

	}

	public class PapersController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Papers.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Papers.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Papers.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Papers.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Papers.Action5");
		}

	}

	public class QueensController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Queens.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Queens.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Queens.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Queens.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Queens.Action5");
		}

	}

	public class QuestionsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Questions.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Questions.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Questions.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Questions.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Questions.Action5");
		}

	}

	public class QuiltsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Quilts.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Quilts.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Quilts.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Quilts.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Quilts.Action5");
		}

	}

	public class QuincesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Quinces.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Quinces.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Quinces.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Quinces.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Quinces.Action5");
		}

	}

	public class RabbitsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Rabbits.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Rabbits.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Rabbits.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Rabbits.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Rabbits.Action5");
		}

	}

	public class RadiatorsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Radiators.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Radiators.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Radiators.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Radiators.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Radiators.Action5");
		}

	}

	public class RadishesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Radishes.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Radishes.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Radishes.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Radishes.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Radishes.Action5");
		}

	}

	public class RainbowsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Rainbows.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Rainbows.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Rainbows.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Rainbows.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Rainbows.Action5");
		}

	}

	public class SailorsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Sailors.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Sailors.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Sailors.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Sailors.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Sailors.Action5");
		}

	}

	public class SalamandersController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Salamanders.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Salamanders.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Salamanders.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Salamanders.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Salamanders.Action5");
		}

	}

	public class SardinesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Sardines.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Sardines.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Sardines.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Sardines.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Sardines.Action5");
		}

	}

	public class SausagesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Sausages.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Sausages.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Sausages.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Sausages.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Sausages.Action5");
		}

	}

	public class TablesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Tables.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Tables.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Tables.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Tables.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Tables.Action5");
		}

	}

	public class TadpolesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Tadpoles.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Tadpoles.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Tadpoles.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Tadpoles.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Tadpoles.Action5");
		}

	}

	public class TaxisController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Taxis.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Taxis.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Taxis.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Taxis.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Taxis.Action5");
		}

	}

	public class TeachersController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Teachers.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Teachers.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Teachers.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Teachers.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Teachers.Action5");
		}

	}

	public class UmbrellasController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Umbrellas.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Umbrellas.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Umbrellas.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Umbrellas.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Umbrellas.Action5");
		}

	}

	public class UnclesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Uncles.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Uncles.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Uncles.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Uncles.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Uncles.Action5");
		}

	}

	public class UnderpantsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Underpants.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Underpants.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Underpants.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Underpants.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Underpants.Action5");
		}

	}

	public class UnitsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Units.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Units.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Units.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Units.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Units.Action5");
		}

	}

	public class ValleysController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Valleys.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Valleys.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Valleys.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Valleys.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Valleys.Action5");
		}

	}

	public class ValuesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Values.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Values.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Values.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Values.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Values.Action5");
		}

	}

	public class VansController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Vans.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Vans.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Vans.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Vans.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Vans.Action5");
		}

	}

	public class WalletsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Wallets.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Wallets.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Wallets.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Wallets.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Wallets.Action5");
		}

	}

	public class WaterfallsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Waterfalls.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Waterfalls.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Waterfalls.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Waterfalls.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Waterfalls.Action5");
		}

	}

	public class WavesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Waves.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Waves.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Waves.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Waves.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Waves.Action5");
		}

	}

	public class WeedsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Weeds.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Weeds.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Weeds.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Weeds.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Weeds.Action5");
		}

	}

	public class XylophonesController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Xylophones.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Xylophones.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Xylophones.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Xylophones.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Xylophones.Action5");
		}

	}

	public class YachtsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Yachts.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Yachts.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Yachts.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Yachts.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Yachts.Action5");
		}

	}

	public class YaksController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Yaks.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Yaks.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Yaks.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Yaks.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Yaks.Action5");
		}

	}

	public class YardsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Yards.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Yards.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Yards.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Yards.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Yards.Action5");
		}

	}

	public class YearsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Years.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Years.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Years.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Years.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Years.Action5");
		}

	}

	public class YamsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Yams.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Yams.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Yams.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Yams.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Yams.Action5");
		}

	}

	public class ZebrasController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Zebras.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Zebras.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Zebras.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Zebras.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Zebras.Action5");
		}

	}

	public class ZephyrsController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Zephyrs.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Zephyrs.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Zephyrs.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Zephyrs.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Zephyrs.Action5");
		}

	}

	public class ZippersController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Zippers.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Zippers.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Zippers.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Zippers.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Zippers.Action5");
		}

	}

	public class ZoosController : Controller
	{
		public ActionResult Action1()
		{
			return Content("Zoos.Action1");
		}

		public ActionResult Action2()
		{
			return Content("Zoos.Action2");
		}

		public ActionResult Action3()
		{
			return Content("Zoos.Action3");
		}

		public ActionResult Action4()
		{
			return Content("Zoos.Action4");
		}

		public ActionResult Action5()
		{
			return Content("Zoos.Action5");
		}

	}
}