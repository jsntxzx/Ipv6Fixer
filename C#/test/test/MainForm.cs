/*
 * Created by SharpDevelop.
 * User: xiang
 * Date: 2014/10/6
 * Time: 16:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        	{
            	if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet) 
           	 	{
                	comboBox1.Items.Add(nic.Name);
            	}
        	}
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void ResetNetwork()
		{
			//interfacename = "以太网";
			string line1 = "netsh interface set interface "+ interfacename + " DISABLED";
			string line2 = "netsh interface set interface "+ interfacename + " ENABLED";
			
			string output1;
			CmdHelper.RunCmd(line1, out output1);
			textBox2.AppendText("executing command " + line1 + "\n");
			textBox2.ScrollToCaret(); 
	//		System.Threading.Thread.Sleep(1000);
			
			string output2;			
			CmdHelper.RunCmd(line2, out output2);
			textBox2.AppendText("executing command " + line2 + "\n");
			textBox2.ScrollToCaret(); 
		
		}
		
		int TestIPv6()
		{
			string line = "ping bt.neu6.edu.cn";
			string output;
			CmdHelper.RunCmd(line, out output);
			Regex regex = new Regex("2001"); 
 
    		if (regex.IsMatch(output)) 
    		{
    			textBox2.AppendText("IPv6 connected!\n");
    			textBox2.ScrollToCaret(); 
    			return 1;
    		}
		    textBox2.AppendText("IPv6 disconnected!\n");
		    textBox2.ScrollToCaret(); 
		    return 0;
		}
		
		async void Foo()
		{
		//	MessageBox.Show(interfacename);
			while(flag)
			{
   		 		int result = TestIPv6();
				if (result == 0)
				{
					ResetNetwork();
				}
				await Task.Delay(int.Parse(textBox1.Text)*1000);
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{
            textBox2.Clear();
 			flag = true;
			textBox2.AppendText("beginning !\n");
			textBox2.ScrollToCaret();
            button1.Enabled = false;
			Foo();
			//	System.Threading.Thread.Sleep(1000);
		}
				
		void Button2Click(object sender, EventArgs e)
		{
			flag = false ;
			textBox2.AppendText("finishing !\n");
			textBox2.ScrollToCaret();
            button1.Enabled = true;
		}
		
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			interfacename = comboBox1.SelectedItem.ToString();

		}

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            } 
        }
		
		
	}
}
