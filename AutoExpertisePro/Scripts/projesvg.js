/*!
  * NKÜ CMF Oto Ekspertiz Projesi için SVG renklendirme scripti
  */






function dropdowndegisimi(id,value)
{
	alert(id + " " + value + " olarak değişti.");

	var parca;
	var durum;

	//Parçanın hangisi olduğunu çözmek için switch-case
	switch(id) 
{
    case "durum_kaput":
        parca = "kaput";
        break;	
    case "durum_bkapak":
        parca = "bagaj_kapagi";
        break;
    case "durum_tavan":
        parca = "tavan";
        break;
    case "durum_ontampon":
        parca = "tampon_on";
        break;
        case "durum_arkatampon":
        parca = "tampon_arka";
        break;
    case "durum_soloncamurluk":
        parca = "camurluk_on_sol";
        break;
    case "durum_solonkapi":
        parca = "kapi_on_sol";
        break;
    case "durum_solarkakapi":
        parca = "kapi_arka_sol";
        break;
    case "durum_solarkacamurluk":
        parca = "camurluk_arka_sol";
        break;
    case "durum_sagoncamurluk":
        parca = "camurluk_on_sag";
        break;
    case "durum_sagonkapi":
        parca = "kapi_on_sag";
        break;
    case "durum_sagarkakapi":
        parca = "kapi_arka_sag";
        break;
    case "durum_sagarkacamurluk":
        parca = "camurluk_arka_sag";
        break;




    default:
        parca = "tavan";
}




	if(value=="Değişen")
	{
		durum = "parca_degisen";
	}
	else if(value=="Boyalı")
	{
		durum = "parca_boyali";
	}
	else if(value=="Orjinal")
	{
		durum = "parca_orjinal";
	}
	else
	{
		durum ="";
	}


	var e1 = document.getElementById(parca);

	e1.setAttribute("class",durum);

}






