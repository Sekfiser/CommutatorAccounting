using System.Diagnostics;
using CommutatorAccounting.Models;
using CommutatorAccounting.Models.SortColumns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CommutatorAccounting.Controllers
{
    public class CommutatorController : Controller
    {
        CommutatorsContext db;

        private readonly ILogger<CommutatorController> _logger;

        public CommutatorController(ILogger<CommutatorController> logger, CommutatorsContext db)
        {
            _logger = logger;
            this.db = db;
            if (!db.Commutators.Any())
            {
                var coms = new Commutator[255]; 

                for (int i = 1; i <= 255; i++){
                    coms[i-1] = new Commutator { 
                        Model = $"TP-Link C-{i}",
                        Ip = $"{i}.{i}.{i}.{i}",
                        Mac = $"A{i % 10}:B{i % 10}:C{i % 10}:D{i % 10}:E{i % 10}:F{i % 10}",
                        Vlan = $"{i}",
                        SerialNumber = $"CDN0{i}0{i}0",
                        StockNumber = $"1{i}0{i}1{i}35",
                        PurchaseDate = DateTime.Now.AddDays(i),
                        InstallationDate = DateTime.Now.AddDays(i+1),
                        InstallationFloor = $"{i%11}",
                        Comment = $"Комментарий {i}"
                    };

                }   

                db.Commutators.AddRange(coms);
                db.SaveChanges();
            }
        }
        public async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 7, SortCommutatorColumns sortColumn = SortCommutatorColumns.IdAsc) {
            IQueryable<Commutator>? commutators = db.Commutators;

            if (!string.IsNullOrEmpty(searchString))
            {
                commutators = commutators.Where(c => EF.Functions.Like(c.Id.ToString(), $"%{searchString}%")
                                                || EF.Functions.Like(c.Ip, $"%{searchString}%")
                                                || EF.Functions.Like(c.Model, $"%{searchString}%")
                                                || EF.Functions.Like(c.Mac, $"%{searchString}%")
                                                || EF.Functions.Like(c.Vlan, $"%{searchString}%")
                                                || EF.Functions.Like(c.SerialNumber, $"%{searchString}%")
                                                || EF.Functions.Like(c.StockNumber, $"%{searchString}%")
                                                || EF.Functions.Like(c.InstallationDate.ToString(), $"%{searchString}%")
                                                || EF.Functions.Like(c.InstallationFloor, $"%{searchString}%")
                                                || EF.Functions.Like(c.PurchaseDate.ToString(), $"%{searchString}%")
                    );
                ViewBag.SearchString = searchString;
            }

            switch (sortColumn)
            {
                case SortCommutatorColumns.IdDesc:
                    commutators = commutators.OrderByDescending(c => c.Id);
                    break;
                case SortCommutatorColumns.IpAsc:
                    commutators = commutators.OrderBy(c => c.Ip);
                    break;
                case SortCommutatorColumns.IpDesc:
                    commutators = commutators.OrderByDescending(c => c.Ip);
                    break;
                case SortCommutatorColumns.ModelAsc:
                    commutators = commutators.OrderBy(c => c.Model);
                    break;
                case SortCommutatorColumns.ModelDesc:
                    commutators = commutators.OrderByDescending(c => c.Model);
                    break;
                case SortCommutatorColumns.MacAsc:
                    commutators = commutators.OrderBy(c => c.Mac);
                    break;
                case SortCommutatorColumns.MacDesc:
                    commutators = commutators.OrderByDescending(c => c.Mac);
                    break;
                case SortCommutatorColumns.VlanAsc:
                    commutators = commutators.OrderBy(c => c.Vlan);
                    break;
                case SortCommutatorColumns.VlanDesc:
                    commutators = commutators.OrderByDescending(c => c.Vlan);
                    break;
                case SortCommutatorColumns.SerialNumberAsc:
                    commutators = commutators.OrderBy(c => c.SerialNumber);
                    break;
                case SortCommutatorColumns.SerialNumberDesc:
                    commutators = commutators.OrderByDescending(c => c.SerialNumber);
                    break;
                case SortCommutatorColumns.StockNumberAsc:
                    commutators = commutators.OrderBy(c => c.StockNumber);
                    break;
                case SortCommutatorColumns.StockNumberDesc:
                    commutators = commutators.OrderByDescending(c => c.StockNumber);
                    break;
                case SortCommutatorColumns.PurchaseDateAsc:
                    commutators = commutators.OrderBy(c => c.PurchaseDate);
                    break;
                case SortCommutatorColumns.PurchaseDateDesc:
                    commutators = commutators.OrderByDescending(c => c.PurchaseDate);
                    break;
                case SortCommutatorColumns.InstallationDateAsc:
                    commutators = commutators.OrderBy(c => c.InstallationDate);
                    break;
                case SortCommutatorColumns.InstallationDateDesc:
                    commutators = commutators.OrderByDescending(c => c.InstallationDate);
                    break;
                case SortCommutatorColumns.InstallationFloorAsc:
                    commutators = commutators.OrderBy(c => c.InstallationFloor);
                    break;
                case SortCommutatorColumns.InstallationFloorDesc:
                    commutators = commutators.OrderByDescending(c => c.InstallationFloor);
                    break;
                default:
                    commutators = commutators.OrderBy(c => c.Id);
                    break;
            }

            var count = await commutators.CountAsync();
            var commutatorsList = await commutators.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageCommutatorModel pageViewModel = new PageCommutatorModel(count, page, pageSize);
            CommutatorExtendedModel commutatorPage = new CommutatorExtendedModel(
                commutatorsList,
                new PageCommutatorModel(count, page, pageSize),
                new FilterCommutatorModel(commutatorsList),
                new SortCommutatorModel(sortColumn)
                );

            return View(commutatorPage);
        }
        public IActionResult Pages()
        {
            return PartialView();
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCommutator(Commutator commutator)
        {
            if (ModelState.IsValid)
            {
                db.Commutators.Add(commutator);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {   
                ViewBag.ModelState = ModelState;
                return View("Add", commutator);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, string callbackLink = "~/")
        {
            if (id != null)
            {
                Commutator commutator = new Commutator { Id = id.Value };
                db.Entry(commutator).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return LocalRedirect(callbackLink);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, string callbackLink = "~/")
        {
            if (id != null)
            {
                Commutator? commutator = await db.Commutators.FirstOrDefaultAsync(p => p.Id == id);
                ViewBag.CallbackLink = callbackLink;
                if (commutator != null) return View(commutator);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Commutator commutator, string callbackLink = "~/")
        {
            if (ModelState.IsValid)
            {
                db.Commutators.Update(commutator);
                await db.SaveChangesAsync();
                return LocalRedirect(callbackLink);
            }
            else 
            {
                ViewBag.ModelState = ModelState;
                return View("Edit", commutator);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<ActionResult> AdvancedSearch(Commutator? commutator = null, int page = 1, int pageSize = 7, SortCommutatorColumns sortColumn = SortCommutatorColumns.IdAsc) {
            IQueryable<Commutator>? commutators = db.Commutators;

            if (commutator.Id.HasValue)
                commutators = commutators.Where(c => c.Id == commutator.Id);
            if (commutator.Ip?.Length > 0)
                commutators = commutators.Where(c => EF.Functions.Like(c.Ip, $"%{commutator.Ip}%"));
            if (commutator.Model?.Length > 0)
                commutators = commutators.Where(c => EF.Functions.Like(c.Model, $"%{commutator.Model}%"));
            if (commutator.Mac?.Length > 0)
                commutators = commutators.Where(c => EF.Functions.Like(c.Mac, $"%{commutator.Mac}%"));
            if (commutator.Vlan?.Length > 0)
                commutators = commutators.Where(c => EF.Functions.Like(c.Vlan, $"%{commutator.Vlan}%"));
            if (commutator.SerialNumber?.Length > 0)
                commutators = commutators.Where(c => EF.Functions.Like(c.SerialNumber, $"%{commutator.SerialNumber}%"));
            if (commutator.StockNumber?.Length > 0)
                commutators = commutators.Where(c => c.StockNumber == commutator.StockNumber);
            if (commutator.InstallationDate.ToString()?.Length > 0)
                commutators = commutators.Where(c => c.InstallationDate == commutator.InstallationDate);
            if (commutator.PurchaseDate.ToString()?.Length > 0)
                commutators = commutators.Where(c => c.PurchaseDate == commutator.PurchaseDate);
            if (commutator.InstallationFloor?.Length > 0)
                commutators = commutators.Where(c => c.InstallationFloor == commutator.InstallationFloor);

            switch (sortColumn)
            {
                case SortCommutatorColumns.IdDesc:
                    commutators = commutators.OrderByDescending(c => c.Id);
                    break;
                case SortCommutatorColumns.IpAsc:
                    commutators = commutators.OrderBy(c => c.Ip);
                    break;
                case SortCommutatorColumns.IpDesc:
                    commutators = commutators.OrderByDescending(c => c.Ip);
                    break;
                case SortCommutatorColumns.ModelAsc:
                    commutators = commutators.OrderBy(c => c.Model);
                    break;
                case SortCommutatorColumns.ModelDesc:
                    commutators = commutators.OrderByDescending(c => c.Model);
                    break;
                case SortCommutatorColumns.MacAsc:
                    commutators = commutators.OrderBy(c => c.Mac);
                    break;
                case SortCommutatorColumns.MacDesc:
                    commutators = commutators.OrderByDescending(c => c.Mac);
                    break;
                case SortCommutatorColumns.VlanAsc:
                    commutators = commutators.OrderBy(c => c.Vlan);
                    break;
                case SortCommutatorColumns.VlanDesc:
                    commutators = commutators.OrderByDescending(c => c.Vlan);
                    break;
                case SortCommutatorColumns.SerialNumberAsc:
                    commutators = commutators.OrderBy(c => c.SerialNumber);
                    break;
                case SortCommutatorColumns.SerialNumberDesc:
                    commutators = commutators.OrderByDescending(c => c.SerialNumber);
                    break;
                case SortCommutatorColumns.StockNumberAsc:
                    commutators = commutators.OrderBy(c => c.StockNumber);
                    break;
                case SortCommutatorColumns.StockNumberDesc:
                    commutators = commutators.OrderByDescending(c => c.StockNumber);
                    break;
                case SortCommutatorColumns.PurchaseDateAsc:
                    commutators = commutators.OrderBy(c => c.PurchaseDate);
                    break;
                case SortCommutatorColumns.PurchaseDateDesc:
                    commutators = commutators.OrderByDescending(c => c.PurchaseDate);
                    break;
                case SortCommutatorColumns.InstallationDateAsc:
                    commutators = commutators.OrderBy(c => c.InstallationDate);
                    break;
                case SortCommutatorColumns.InstallationDateDesc:
                    commutators = commutators.OrderByDescending(c => c.InstallationDate);
                    break;
                case SortCommutatorColumns.InstallationFloorAsc:
                    commutators = commutators.OrderBy(c => c.InstallationFloor);
                    break;
                case SortCommutatorColumns.InstallationFloorDesc:
                    commutators = commutators.OrderByDescending(c => c.InstallationFloor);
                    break;
                default:
                    commutators = commutators.OrderBy(c => c.Id);
                    break;
            }

            ViewBag.Action = "AdvancedSearch";

            var count = await commutators.CountAsync();
            var commutatorsList = await commutators.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageCommutatorModel pageViewModel = new PageCommutatorModel(count, page, pageSize);
            CommutatorExtendedModel commutatorPage = new CommutatorExtendedModel(
                commutatorsList,
                new PageCommutatorModel(count, page, pageSize),
                new FilterCommutatorModel(commutatorsList),
                new SortCommutatorModel(sortColumn),
                commutator
                );

            return View(commutatorPage);
        }
    }
}