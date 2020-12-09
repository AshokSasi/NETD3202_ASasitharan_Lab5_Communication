/*
 * Name: Ashok Sasitharan
 * ID:100745484
 * Date: December 1 2020
 * Project: NETD3202 Lab5 Comm2
 * File: PatientsController.cs
 * Purpose: This file is the patients controller and checks if the user input is valid before putting it into the database
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NETD3202_ASasitharan_Lab5_Comm2.Models;
using NETD3202_ASasitharan_Lab5_Comm2.Data;
using Microsoft.AspNetCore.Authorization;

namespace NETD3202_ASasitharan_Lab5.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patients
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.patients.ToListAsync());
        }

        // GET: Patients/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.patients
                .FirstOrDefaultAsync(m => m.patientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("patientId,patientfname,patientlname,patientphone")] Patient patient)
        {
            //check if the user input is valid through a function and put input in database if it is valid else show fail screen
            if (patient.Validate(patient.patientfname, patient.patientlname, patient.patientphone) == false)
            {
                return View("Fail");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(patient);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(patient);
            }
           
        }

        // GET: Patients/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("patientId,patientfname,patientlname,patientphone")] Patient patient)
        {
            if (id != patient.patientId)
            {
                return NotFound();
            }
            if (patient.Validate(patient.patientfname, patient.patientlname, patient.patientphone) == false)
            {
                return View("Fail");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(patient);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PatientExists(patient.patientId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(patient);
            }
            
        }

        // GET: Patients/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.patients
                .FirstOrDefaultAsync(m => m.patientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.patients.FindAsync(id);
            _context.patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.patients.Any(e => e.patientId == id);
        }
    }
}
