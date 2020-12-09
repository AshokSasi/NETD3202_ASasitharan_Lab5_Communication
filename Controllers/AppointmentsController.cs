/*
 * Name: Ashok Sasitharan
 * ID:100745484
 * Date: December 1 2020
 * Project: NETD3202 Lab5
 * File: AppointmentsController.cs
 * Purpose: This file is the appointments controller and checks if the user input is valid before putting it into the database
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
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.appointment.Include(a => a.Doctor).Include(a => a.Patient);
           

            return View(await hospitalContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointment
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.appointmentId == id);
            
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize]
        public IActionResult Create()
        {
            //set the alias of doctor ID and patient ID to the full name of the doctor or patient so the user can see who the ID belongs too
            ViewData["doctorId"] = new SelectList(_context.doctors, "doctorId", "fullname");
            ViewData["patientId"] = new SelectList(_context.patients, "patientId", "patientfullname");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("appointmentId,appointmentType,appointmentDate,doctorfname,doctorId,patientId")] Appointment appointment)
        {
            //put the appointment type into a validate function to check its string length
            if (appointment.Validate(appointment.appointmentType) == false)
            {
                return View("Fail");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(appointment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                //set the alias of doctor ID and patient ID to the full name of the doctor or patient so the user can see who the ID belongs too
                ViewData["doctorId"] = new SelectList(_context.doctors, "doctorId", "fullname", appointment.doctorId);
                ViewData["patientId"] = new SelectList(_context.patients, "patientId", "patientfullname", appointment.patientId);
                return View(appointment);
            }
            
        }

        // GET: Appointments/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            //set the alias of doctor ID and patient ID to the full name of the doctor or patient so the user can see who the ID belongs too
            ViewData["doctorId"] = new SelectList(_context.doctors, "doctorId", "fullname", appointment.doctorId);
            ViewData["patientId"] = new SelectList(_context.patients, "patientId", "patientfullname", appointment.patientId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("appointmentId,appointmentType,appointmentDate,doctorId,patientId")] Appointment appointment)
        {
            if (id != appointment.appointmentId)
            {
                return NotFound();
            }
            if (appointment.Validate(appointment.appointmentType) == false)
            {
                return View("Fail");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(appointment);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AppointmentExists(appointment.appointmentId))
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
                //set the alias of doctor ID and patient ID to the full name of the doctor or patient so the user can see who the ID belongs too
                ViewData["doctorId"] = new SelectList(_context.doctors, "doctorId", "fullname", appointment.doctorId);
                ViewData["patientId"] = new SelectList(_context.patients, "patientId", "patientfullname", appointment.patientId);
                return View(appointment);
            }
            
        }

        // GET: Appointments/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointment
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.appointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.appointment.FindAsync(id);
            _context.appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.appointment.Any(e => e.appointmentId == id);
        }
    }
}
