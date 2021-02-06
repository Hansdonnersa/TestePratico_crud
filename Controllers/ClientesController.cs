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
    public class ClientesController : Controller
    {
        private testepraticoEntities2 db = new testepraticoEntities2();


        public async Task<ActionResult> Index()
        {
            return View(await db.Cliente.ToListAsync());
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            return View(cliente);
        }


        public ActionResult Create()
        {
            return View();
        }

        
        public async Task<ActionResult> Create([Bind(Include = "Identificador,Nome,CPF,RG,DataNascimento,Ativo")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

      
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        public async Task<ActionResult> Edit([Bind(Include = "Identificador,Nome,CPF,RG,DataNascimento,Ativo")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

     
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cliente cliente = await db.Cliente.FindAsync(id);
            db.Cliente.Remove(cliente);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
