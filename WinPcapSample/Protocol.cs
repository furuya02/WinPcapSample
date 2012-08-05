using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace WinPcapSample {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct EtherHeader {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] dstMac;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] srcMac;
        public ushort type;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct IpHeader {
        public byte verLen;
        public byte tos;
        public ushort totalLen;
        public short ident;
        public short flags;
        public byte ttl;
        public byte protocol;
        public short checkSum;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] srcIp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] dstIp;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TcpHeader {
        public ushort srcPort;
        public ushort dstPort;
        public uint squence;
        public uint ack;
        public byte offset;
        public byte flg;
        public short window;
        public short checkSum;
        public short urgent;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct UdpHeader {
        public ushort srcPort;
        public ushort dstPort;
        public short udpLen;
        public short checkSum;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ArpHeader {
        public short hwType;
        public short type;
        public byte hwLen;
        public byte protoLen;
        public short code;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] srcMac;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] srcIp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] dstMac;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] dstIp;
    }

    enum Protocol : byte {
        Reserved = 0,
        ICMP = 1,
        IGMP = 2,
        GGP = 3,
        IP = 4,
        ST = 5,
        TCP = 6,
        UCL = 7,
        EGP = 8,
        IGP = 9,
        BBN_RCC_MON = 10,
        NVP_II = 11,
        PUP = 12,
        ARGUS = 13,
        EMCON = 14,
        XNET = 15,
        CHAOS = 16,
        UDP = 17,
        MUX = 18,
        DCN_MEAS = 19,
        HMP = 20,
        PRM = 21,
        XNS_IDP = 22,
        TRUNK_1 = 23,
        TRUNK_2 = 24,
        LEAF_1 = 25,
        LEAF_2 = 26,
        RDP = 27,
        IRTP = 28,
        ISO_TP4 = 29,
        NETBLT = 30,
        MFE_NSP = 31,
        MERIT_INP = 32,
        SEP = 33,
        PC = 34,
        IDPR = 35,
        XTP = 36,
        DDP = 37,
        IDPR_CMTP = 38,
        TP__ = 39,
        IL = 40,
        SIP = 41,
        SDRP = 42,
        SIP_SR = 43,
        SIP_FRAG = 44,
        IDRP = 45,
        RSVP = 46,
        GRE = 47,
        MHRP = 48,
        BNA = 49,
        SIPP_ESP = 50,
        SIPP_AH = 51,
        I_NLSP = 52,
        SWIPE = 53,
        NHRP = 54,
        CFTP = 62,
        SAT_EXPAK = 64,
        KRYPTOLAN = 65,
        RVD = 66,
        IPPC = 67,
        SAT_MON = 69,
        VISA = 70,
        IPCV = 71,
        CPNX = 72,
        CPHB = 73,
        WSN = 74,
        PVP = 75,
        BR_SAT_MON = 76,
        SUN_ND = 77,
        WB_MON = 78,
        WB_EXPAK = 79,
        ISO_IP = 80,
        VMTP = 81,
        SECURE_VMTP = 82,
        VINES = 83,
        TTP = 84,
        NSFNET_IGP = 85,
        DGP = 86,
        TCF = 87,
        IGRP = 88,
        OSPFIGP = 89,
        Sprite_RPC = 90,
        LARP = 91,
        MTP = 92,
        AX25 = 93,
        IPIP = 94,
        MICP = 95,
        SCC_SP = 96,
        ETHERIP = 97,
        ENCAP = 98,
        GMTP = 99
    }
}
