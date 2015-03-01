using System;
using System.Collections.Generic;
using System.Text;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EManageCheck
    {
        private string _fcheckid;
        private string _fcheckuser;
        private DateTime _fchecktime;
        private string _fcheckcontent;
        private string _fcompanyid;
        private bool _fisfeleted;
        private int _fchecktype;
        private DateTime _fcreatetime;
        private string _fupdateuid;
        private DateTime _fupdatetime;
        private string _fcreateuid;
        private bool _fispub;

        public bool FIsPub
        {
            set { _fispub = value; }
            get { return _fispub; }
        }
        public string FCheckID
        {
            get { return _fcheckid; }
            set { _fcheckid = value; }
        }

        public string FCheckUser
        {
            set { _fcheckuser = value; }
            get { return _fcheckuser; }
        }

        public string FCheckContent
        {
            set { _fcheckcontent = value; }
            get { return _fcheckcontent; }
        }

        public DateTime FCheckTime
        {
            get { return _fchecktime; }
            set { _fchecktime = value; }
        }

        public string FBaseInfoID
        {
            get { return _fcompanyid; }
            set { _fcompanyid = value; }
        }

        public bool FIsDeleted
        {
            get { return _fisfeleted; }
            set { _fisfeleted = value; }
        }

        public int FCheckType
        {
            get { return _fchecktype; }
            set { _fchecktype = value; }
        }

        public DateTime FCreateTime
        {
            get { return _fcreatetime; }
            set { _fcreatetime = value; }
        }

        public DateTime FUpdateTime
        {
            get { return _fupdatetime; }
            set { _fupdatetime = value; }
        }

        public string FCreateUID
        {
            get { return _fcreateuid; }
            set { _fcreateuid = value; }
        }

        public string FUpdateUID
        {
            get { return _fupdateuid; }
            set { _fupdateuid = value; }
        }
    }
}
