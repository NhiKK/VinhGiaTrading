using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConvertMoney;

public static class VNN_ConvertMoney
{
    public static string convert(double tien, string donvitien)
    {
		string kq = "";
		if(donvitien == "VND"){
			if(tien > 0) {
				MakeToString mk_tien = new MakeToString(tien);
				mk_tien.BlockProcessing();
				kq = mk_tien.ReadThis() + " " + "đồng";
			}
			else {
				kq = "Không " + "đồng";
			}
		}
		else {
			string str_tien = tien.ToString();
			int space = str_tien.LastIndexOf(".");
			double phan_nguyen = tien;
			if(space > 0)
				phan_nguyen = double.Parse(str_tien.Substring(0, space));
			
			double phan_thapphan = (double)Math.Round((tien - phan_nguyen)* 100); 
			if(phan_nguyen > 0)
			{
				MakeToString mk_tien = new MakeToString(Convert.ToDouble(phan_nguyen));
				mk_tien.BlockProcessing();
				kq = mk_tien.ReadThis() + " " + donvitien;
			}
			else {
				phan_thapphan = Math.Abs(phan_thapphan);
			}
			
			if(phan_nguyen <= 0 & phan_thapphan <= 0){
				kq = kq + "Không " + donvitien;
			}
			else if(phan_thapphan > 0) {
				MakeToString mk_tien = new MakeToString(Convert.ToDouble(phan_thapphan));
				mk_tien.BlockProcessing();
				if(phan_nguyen > 0)
					kq = kq + " " + mk_tien.ReadThis().ToLower() + " " + "cent";
				else
					kq = mk_tien.ReadThis() + " " + "cent";
			}
		}
		return kq;
	}
	
	public static decimal autoRound(decimal number, int round){
		string s = Math.Round(number, round).ToString();
		decimal atr = 0;
		if(s.Contains(".")){
			for(int i=s.Length - 1; i >= 0; i--) {
				if(s[i] == '0'){
					s = s.Remove(i);
				}
				else if(s[i] == '.'){
					s= s.Remove(i);
					break;
				}
				else {
					break;
				}
			}
			atr = decimal.Parse(s);
		}
		else {
			atr = number;
		}
		return atr;
	}
	
	public static string sep_thous(string number, string thous, string dec) {
		string vl_ = number.Replace(".", dec);
		int stt = vl_.LastIndexOf(dec);
		double stt_sep = stt/3;
		int stt_ = (int)Math.Round(stt_sep,0);
		if(stt < 0) {
			stt = vl_.Length;
		}
		
		for(int i=stt - 3; i > 0; i -= 3) {
			vl_ = vl_.Insert(i,thous);
		}
		
		return vl_;
	}
}