using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;


namespace WinPcapSample {
    //********************************************************
    // WinPcap
    //********************************************************

    

    class WinPcap {
        const int PCAP_ERRBUF_SIZE = 256;// エラーメッセージのバッファサイズ
        const int MAX_RECV_SIZE = 65535; // 受信バッファのサイズ

        [StructLayout(LayoutKind.Sequential)]
        public struct pcap_if {
            public IntPtr next;
            public string name;
            public string description;
            public IntPtr addresses;
            public uint flags;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct pcap_pkthdr {
            public int tv_sec;
            public int tv_usec;
            public uint caplen;
            public uint len;
        };
        //int pcap_findalldevs(pcap_if_t **, char *);
        [DllImport("wpcap.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)] 
        private static extern int pcap_findalldevs(
            ref IntPtr alldevsp, 
            StringBuilder errbuf
            );

        //void	pcap_freealldevs(pcap_if_t *)
        [DllImport("wpcap.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void pcap_freealldevs(
            IntPtr alldevsp
            );

        
        [DllImport("wpcap.dll",CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private extern static IntPtr pcap_open(
            string dev,
            int packetLen,
            short mode,
            short timeout,
            StringBuilder errbuf);
        
        [DllImport("wpcap.dll",CallingConvention = CallingConvention.Cdecl)]
        static extern int pcap_next_ex(
            IntPtr p,
            ref IntPtr pkt_header,
            ref IntPtr pkt_data
            );

        [DllImport("wpcap.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void pcap_close(
            IntPtr p
            );

        static Thread t;
        static IntPtr handle;
        // デリゲート
        public delegate void OnRecvHandler(IntPtr pkt_hdr, IntPtr pkt_data);
        //イベント
        public static event OnRecvHandler OnRecv;

        static void Loop() {
            IntPtr pkt_data = new IntPtr();
            IntPtr pkt_hdr = new IntPtr();
            while (true) {
                int res = pcap_next_ex(handle, ref pkt_hdr, ref pkt_data);//データ取得
                if (res < 0) { //res==0の場合、受信パケット0でタイムアウト
                    // ERROR!!;
                } else if (res > 0) {
                    if (OnRecv != null) {
                        OnRecv(pkt_hdr, pkt_data);
                    }
                }
                Thread.Sleep(1);
            }
        }
        
        //NICの一覧取得
        public static List<pcap_if> GetDeviceList() {
            var result = new List<pcap_if>();//デバイス一覧
           
            IntPtr alldevs = new IntPtr();//情報取得用のバッファ
            StringBuilder errbuf = new StringBuilder(PCAP_ERRBUF_SIZE);//エラー用バッファ
 
            if (pcap_findalldevs(ref alldevs,errbuf) != -1) {
                IntPtr p = alldevs;
                while (!p.Equals(IntPtr.Zero)) {
                    // pcap_if構造体で参照する
                    pcap_if i = (pcap_if)Marshal.PtrToStructure(p, typeof(pcap_if));
                    result.Add(i);
                    p = i.next; // 次のアダプタ情報にポインタを移動
                }
                // 情報取得用のバッファの開放
                pcap_freealldevs(alldevs);
            }else{
                // エラー (エラーの詳細は、errbufに格納されている)
            }
            return result;
        }
        //キャプチャ開始
        public static bool Start(string deviceName,bool promiscuous) {
            
            short timeout = 20;
            short Promiscast = (short)(promiscuous ? 1 : 0);
            StringBuilder errbuf = new StringBuilder(PCAP_ERRBUF_SIZE);
            handle = pcap_open(deviceName, MAX_RECV_SIZE, Promiscast, timeout,errbuf);
            if (handle.Equals(IntPtr.Zero)) {
                // エラー (エラーの詳細は、errbufに格納されている)
                return false;
            }
            t = new Thread(new ThreadStart(Loop));
            t.IsBackground = true;
            t.Start();
            return true;
        }
        // キャプチャー終了
        public static bool Stop() {
            t.Abort();
            t = null;
            
            pcap_close(handle);//キャプチャ終了
            return true;
        }

    }
}
