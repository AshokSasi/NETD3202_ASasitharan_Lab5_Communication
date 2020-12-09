/*
 * Name: Ashok Sasitharan
 * ID:100745484
 * Date: December 1 2020
 * Project: NETD3202 Lab5 Comm2
 * File: DoctorsController.cs
 * Purpose: This file is the doctor controller and checks if the user input is valid before putting it into the database
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
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Doctors
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.doctors.ToListAsync());
        }

        // GET: Doctors/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.doctors
                .FirstOrDefaultAsync(m => m.doctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("doctorId,doctorfname,doctorlname,doctorphone")] Doctor doctor)
        {
            //put the doctor fields in a validation function to see if the phone number is numeric and if the names a greater than the minimum character length
            if (doctor.Validate(doctor.doctorfname,doctor.doctorlname,doctor.doctorphone) == false)
            {
                return View("Fail");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(doctor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }


                return View(doctor);
            }
            
        }

        // GET: Doctors/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("doctorId,doctorfname,doctorlname,doctorphone")] Doctor doctor)
        {
            if (id != doctor.doctorId)
            {
                return NotFound();
            }
            if (doctor.Validate(doctor.doctorfname, doctor.doctorlname, doctor.doctorphone) == false)
            {
                return View("Fail");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(doctor);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DoctorExists(doctor.doctorId))
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
                return View(doctor);
            }
            
        }

        // GET: Doctors/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.doctors
                .FirstOrDefaultAsync(m => m.doctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.doctors.FindAsync(id);
            _context.doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.doctors.Any(e => e.doctorId == id);
        }
    }
}
