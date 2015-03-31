using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using busca_de_alunos.Models;
using System.Collections.Generic;

namespace busca_de_alunos.Controllers
{
    public class LocalizacaoAlunosController : Controller
    {
        private LocalizacaoAlunoContext db = new LocalizacaoAlunoContext();

        #region Listagem de Alunos e Localizações
        // GET: LocalizacaoAlunos
        public ActionResult Index()
        {
            return View(db.LocalizacaoAluno.ToList());
        }
        #endregion

        #region Cadastrar Aluno e endereço de localização
        // GET: LocalizacaoAlunos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocalizacaoAlunos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Matricula,Nome,Endereco")] LocalizacaoAluno localizacaoAluno)
        {
            if (db.LocalizacaoAluno.Count(x => x.Matricula == localizacaoAluno.Matricula) > 0)
            {
                ModelState.AddModelError("matricula", "A matricula informada já existe.");

                return View(localizacaoAluno);
            }

            if (ModelState.IsValid)
            {
                db.LocalizacaoAluno.Add(localizacaoAluno);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(localizacaoAluno);
        }
        #endregion

        #region Editar Aluno e localização
        // GET: LocalizacaoAlunos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            LocalizacaoAluno localizacaoAluno = db.LocalizacaoAluno.Find(id);

            if (localizacaoAluno == null)
                return HttpNotFound();

            return View(localizacaoAluno);
        }

        // POST: LocalizacaoAlunos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Matricula,Nome,Endereco")] LocalizacaoAluno localizacaoAluno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localizacaoAluno).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(localizacaoAluno);
        }

        #endregion

        #region Excluir Aluno e localização
        // GET: LocalizacaoAlunos/Delete/5
        public ActionResult Delete(string id)
        {
            LocalizacaoAluno localizacaoAluno = db.LocalizacaoAluno.Find(id);

            db.LocalizacaoAluno.Remove(localizacaoAluno);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion

        #region Localizar Aluno especifico pela matrícula
        // GET: LocalizacaoAlunos/Find
        public JsonResult Find(string id)
        {
            LocalizacaoAluno localizacaoAluno = db.LocalizacaoAluno.Find(id);

            if (localizacaoAluno != null)
            {
                CreateFile(localizacaoAluno);

                return Json(localizacaoAluno.Matricula + ";" + localizacaoAluno.Nome + ";" + localizacaoAluno.Endereco, JsonRequestBehavior.AllowGet);
            }

            return Json("Registro não encontrado!", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Listar as últimas localizações pesquisadas
        // GET: LocalizacaoAlunos/Find
        public JsonResult List()
        {

            int counter = 0;
            string line;
            var path = Server.MapPath("~/Data/temp/localizacao.txt");
            // Read the file and display it line by line.
            StreamReader file = new StreamReader(path);

            string localizacoes = "";
            var lines = file.ReadToEnd().Split(new char[] { '\n' });

            var cont = 0;
            var qtd = lines.Length - 1;

            while (cont <= 10 || cont == qtd)
            {
                if (lines[qtd].ToString() != "")
                    localizacoes += lines[qtd].ToString() + "*";

                cont++;
                qtd--;
            }

            file.Close();

            return Json(localizacoes != "" ? localizacoes.ToString() : "Registro não encontrado!", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Criar e Ler o arquivo localizacao.txt
        //Create and read the file
        public void CreateFile(LocalizacaoAluno localizacaoAluno)
        {
            var path = Server.MapPath("~/Data/temp/localizacao.txt");

            if (!System.IO.File.Exists(path))
            {
                using (StreamWriter sw = System.IO.File.CreateText(path))
                {
                    sw.WriteLine(localizacaoAluno.Matricula + ";" + localizacaoAluno.Nome + ";" + localizacaoAluno.Endereco);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(localizacaoAluno.Matricula + ";" + localizacaoAluno.Nome + ";" + localizacaoAluno.Endereco);
                }
            }
        } 
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
