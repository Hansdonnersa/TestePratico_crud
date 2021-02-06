using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestePratico_crud.Models;

namespace TestePratico_crud.Controllers
{
    public class LojasController : Controller
    {
        private testepraticoEntities2 db = new testepraticoEntities2();

        // GET: Lojas
        public async Task<ActionResult> Index()
        {
            return View(await db.Loja.ToListAsync());
        }

        // GET: Lojas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loja loja = await db.Loja.FindAsync(id);
            if (loja == null)
            {
                return HttpNotFound();
            }
            return View(loja);
        }

        // GET: Lojas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lojas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Identificador,Nome")] Loja loja)
        {
            if (ModelState.IsValid)
            {
                db.Loja.Add(loja);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(loja);
        }

        // GET: Lojas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loja loja = await db.Loja.FindAsync(id);
            if (loja == null)
            {
                return HttpNotFound();
            }
            return View(loja);
        }

        // POST: Lojas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Identificador,Nome")] Loja loja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loja).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(loja);
        }
        public async Task<ActionResult> AssociarCliente(int idCliente, int idLoja)
        {
            Loja loja = await db.Loja.FindAsync(idLoja);
            Cliente cliente = await db.Cliente.FindAsync(idCliente);
            loja.Cliente.Add(cliente);

            db.Entry(loja).State = EntityState.Modified;
            await db.SaveChangesAsync();


            ViewBag.IdLoja = idLoja;

            return View("Associar", await db.Cliente.ToListAsync());
        }
        public async Task<ActionResult> DesassociarCliente(int idCliente, int idLoja)
        {
            Loja loja = await db.Loja.FindAsync(idLoja);
            Cliente cliente = await db.Cliente.FindAsync(idCliente);
            loja.Cliente.Remove(cliente);

            db.Entry(loja).State = EntityState.Modified;
            await db.SaveChangesAsync();
        
          
            ViewBag.IdLoja = idLoja;

            return View("Associar",await db.Cliente.ToListAsync());
        }


        // GET: Lojas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loja loja = await db.Loja.FindAsync(id);
            if (loja == null)
            {
                return HttpNotFound();
            }
            return View(loja);
        }

        // POST: Lojas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Loja loja = await db.Loja.FindAsync(id);
            db.Loja.Remove(loja);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Associar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.IdLoja = id;
            return View(await db.Cliente.ToListAsync());
            
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
