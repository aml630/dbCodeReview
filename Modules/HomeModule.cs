using System.Collections.Generic;
using Nancy;
using System;
using Nancy.ViewEngines.Razor;

namespace SalonNamespace
{
  public class HomeModule : NancyModule
  {

    public HomeModule()
    {
      Get["/"] =_=> {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };

      Get["/Stylist/new"] =_=> {
        return View["createStylist.cshtml"];
      };

      Post["/Stylist/new"] =_=> {
        Stylist newStylist = new Stylist(Request.Form["stylistName"]);
        newStylist.Save();
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };

      Get["/Stylist/{id}/edit"] =parameters=> {
        Stylist selectedStylist = Stylist.Find(parameters.id);
        return View["editStylist.cshtml", selectedStylist];
      };

      Patch["/Stylist/Edited/{id}"] =parameters=> {
        Stylist selectedStylist = Stylist.Find(parameters.id);
        selectedStylist.Update(Request.Form["stylistEditName"]);
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };

      Get["/Stylist/{id}/clients"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectedStylist = Stylist.Find(parameters.id);
        var stylistClients = selectedStylist.GetClients();
        model.Add("stylist", selectedStylist);
        model.Add("clientList", stylistClients);
        return View["clientList.cshtml", model];
      };

      Get["/Client/add/{id}"] = parameters => {
        Stylist selectedStylist = Stylist.Find(parameters.id);
        return View["createClient.cshtml", selectedStylist];
      };

      Post["/Client/new/{id}"] =parameters=> {
        Stylist selectedStylist = Stylist.Find(parameters.id);
        int id = selectedStylist.GetID();
        Client newClient = new Client(Request.Form["clientName"], id);
        newClient.Save();
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };

      Get["/Stylist/{id}/clients/edit"] =parameters=> {
        Client selectedClient = Client.Find(parameters.id);
        Console.WriteLine("Selected client name: " + selectedClient);
        Console.WriteLine("Selected client id:  " + selectedClient.GetName());
        return View["editClient.cshtml", selectedClient];
      };

      Patch["/Stylist/{id}/clients/edited"] =parameters=> {
        Client selectedClient = Client.Find(parameters.id);
        selectedClient.UpdateName(Request.Form["clientEditName"]);
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };

      Delete["/Stylist/{id}/clients/deleted"] = parameters => {
        Client selectedClient = Client.Find(parameters.id);
        selectedClient.Delete();
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };

      Delete["/Stylist/{id}/delete"] = parameters => {
        Stylist selectedStylist = Stylist.Find(parameters.id);
        selectedStylist.Delete();
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };
    }
  }
}
