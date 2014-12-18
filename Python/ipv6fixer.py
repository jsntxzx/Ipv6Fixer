
# coding : utf-8
# author : xiang

import os
import time
import subprocess
import re

line1 = u'netsh interface set interface 以太网 DISABLED'
line2 = u'netsh interface set interface 以太网 ENABLED'
line3 = 'ping bt.neu6.edu.cn'

cmdcodingline = 'chcp 437'

def ResetAdapter():
     print 'executing command', line1.encode('gbk')
     os.system(line1.encode('gbk'))
     time.sleep(3)
     print 'executing command', line2.encode('gbk')
     os.system(line2.encode('gbk'))
     time.sleep(3)

def TestIPv6():
     netcmd = subprocess.Popen(line3, shell=True, stderr=subprocess.PIPE,stdout=subprocess.PIPE)
     output,err = netcmd.communicate()
     pattern = re.compile(r'2001',re.DOTALL)
     match = pattern.findall(output)
     if len(match) > 0 :
          print 'ipv6 network is fine'
          return 1
     else:
          print 'ipv6 network is disconnected'
          return 0



if __name__ == '__main__':
#     os.system(cmdcodingline)
     while(True):
          result = TestIPv6()
          if result == 0 :
               ResetAdapter()
          # check every 5 minutes
          time.sleep(300)
