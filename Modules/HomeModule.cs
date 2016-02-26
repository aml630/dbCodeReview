using System.Collections.Generic;
using Nancy;
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

      Delete["/Stylist/{id}/delete"] = parameters => {
        Stylist selectedStylist = Stylist.Find(parameters.id);
        selectedStylist.Delete();
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };

    }

  }

}
