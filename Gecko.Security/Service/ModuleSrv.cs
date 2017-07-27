using System;
using System.Collections;
using NHibernate;
using Gecko.Security.NHHelper;
using Gecko.Security.Domain;
using Gecko.Security.DTO;

namespace Gecko.Security.Service
{
    /// <summary>
    /// ģ�顣
    /// </summary>
    public sealed class ModuleSrv
    {
        private ModuleSrv() { }

        #region public static Module GetModuleByTag(string tag)
        /// <summary>
        /// ���ݱ�ʾ��ȡģ�顣
        /// </summary>
        /// <param name="tag">��ʾ��</param>
        /// <returns>ģ�顣</returns>
        public static Module GetModuleByTag(string tag)
        {
            string hql = "from Module m where m.Tag = :tag";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetString("tag", tag);
            q.SetCacheable(true);
            q.SetCacheRegion("Module");
            object o = q.UniqueResult();
            return (o != null) ? (Module)o : null;
        }
        #endregion

        #region public static IList GetAllDisabledModule()

        /// <summary>
        /// ��ȡ������Чδ���õ�ģ���б�
        /// </summary>
        /// <param name="tag">��ʾ��</param>
        /// <returns>ģ�顣</returns>
        public static IList GetAllDisabledModule()
        {
            string hql = "from Module m where m.Disabled = :disabled";
            IQuery q = Db.Session.CreateQuery(hql);
            q.SetInt32("disabled", 0);
            q.SetCacheable(true);
            q.SetCacheRegion("Module");
            return q.List();
        }

        #endregion

        #region public static void DeleteModule(string id)
        /// <summary>
        /// ɾ��ģ�顣
        /// </summary>
        /// <param name="id">Ҫɾ����ģ�������ֵ��</param>
        public static void DeleteModule(string id)
        {
            Db.SessionFactory.EvictQueries("Module");

            Module m = Db.Session.Load(typeof(Module), id) as Module;
            m.BreakAwayFromModuleType();

            Db.TransDelete(m);
        }
        #endregion

        #region public static string InsertModule(ModuleDTO dto)
        /// <summary>
        /// ����ģ�顣
        /// </summary>
        /// <param name="dto">������ģ�����Ϣ��</param>
        /// <returns>��ģ���Id����-2��Tag�ظ�����</returns>
        public static string InsertModule(ModuleDTO dto)
        {
            Module existingM = GetModuleByTag(dto.Tag);
            if (existingM != null) { return "-2"; }

            Db.SessionFactory.EvictQueries("Module");

            //ģ�������Ϣ��
            Module m = new Module();
            m.Id = IdGen.GetNextId(typeof(Module));
            m.Tag = dto.Tag;
            m.Name = dto.Name;
            m.Remark = dto.Remark;
            m.OrderId = dto.OrderId;
            m.ModuleUrl = dto.ModuleUrl;
            m.Disabled = dto.Disabled?1:0;

            //ģ����ࡣ
            ModuleType mt = Db.Session.Load(typeof(ModuleType), dto.ModuleTypeId) as ModuleType;
            mt.AddModule(m);

            //��ȡ����ģ��Ȩ�޵�����ֵ��
            string[] Ids = null;
            if (dto.ModuleRights.Count > 0)
                Ids = IdGen.GetNextId(typeof(ModuleRight), dto.ModuleRights.Count);

            //�����ݿⱣ��ģ���ģ��Ȩ�ޡ�
            ITransaction transaction = Db.Session.BeginTransaction();
            try
            {
                Db.Session.Save(m);
                for (int i = 0; i < dto.ModuleRights.Count; i++)
                {
                    ModuleRight mr = new ModuleRight();
                    mr.Id = Ids[i];
                    mr.RightTag = dto.ModuleRights[i].ToString();
                    m.AddModuleRight(mr);
                    Db.Session.Save(mr);
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

            //������ģ���Id��
            return m.Id;
        }
        #endregion

        #region public static string UpdateModule(ModuleDTO dto)
        /// <summary>
        /// ����ģ�顣
        /// </summary>
        /// <param name="dto">������ģ�����Ϣ��</param>
        /// <returns>�ɹ���ʾ����1���ɹ���-2��Tag�ظ�����</returns>
        public static string UpdateModule(ModuleDTO dto)
        {
            Module existingM = GetModuleByTag(dto.Tag);
            if (existingM != null && existingM.Id != dto.Id) { return "-2"; }

            Db.SessionFactory.EvictQueries("Module");

            //ģ�������Ϣ��
            Module m = Db.Session.Load(typeof(Module), dto.Id) as Module;
            m.Tag = dto.Tag;
            m.Name = dto.Name;
            m.Remark = dto.Remark;
            m.OrderId = dto.OrderId;
            m.ModuleUrl = dto.ModuleUrl;
            m.Disabled = dto.Disabled?1:0;

            //��ɾ����Ȩ�ޡ�
            ArrayList ToBeRemoved = new ArrayList();

            foreach (var k in m.ModuleRights)
            {
                string key = k.Key.ToString();
                bool isFind = false;
                for (int i = 0; i < dto.ModuleRights.Count; i++)
                {
                    if (key == dto.ModuleRights[i].ToString())
                    {
                        isFind = true;
                        break;
                    }
                }
                if (!isFind)
                {
                    ToBeRemoved.Add(key);
                }
            }


            //�����ӵ�Ȩ�ޡ�
            ArrayList ToBeAdded = new ArrayList();
            for (int i = 0; i < dto.ModuleRights.Count; i++)
            {
                //2017-07-20 �޸�
                if (!m.ModuleRights.ContainsKey(dto.ModuleRights[i]))
                    ToBeAdded.Add(dto.ModuleRights[i].ToString());
            }

            //��ȡ����ģ��Ȩ�޵�����ֵ��
            string[] Ids = null;
            if (ToBeAdded.Count > 0)
                Ids = IdGen.GetNextId(typeof(ModuleRight), ToBeAdded.Count);

            //�����ݿⱣ���µ�ģ����Ϣ��ģ��Ȩ����Ϣ��
            ITransaction transaction = Db.Session.BeginTransaction();
            try
            {
                //ɾ��Ȩ�ޡ�
                foreach (object key in ToBeRemoved)
                {
                    ModuleRight mr = m.ModuleRights[key] as ModuleRight;
                    mr.RemoveAllPermissions();
                    mr.BreakAwayFromModule();
                    Db.Session.Delete(mr);
                }

                //����Ȩ�ޡ�
                for (int i = 0; i < ToBeAdded.Count; i++)
                {
                    ModuleRight newMr = new ModuleRight();
                    newMr.Id = Ids[i];
                    newMr.RightTag = ToBeAdded[i].ToString();
                    m.AddModuleRight(newMr);
                    Db.Session.Save(newMr);
                }

                Db.Session.Update(m);
                transaction.Commit();

                return "1";
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        #endregion

        #region public static void MoveModule(string id, string newModuleTypeId)
        /// <summary>
        /// �ƶ�ģ�顣
        /// </summary>
        /// <param name="id">Ҫ�ƶ���ģ���ID��</param>
        /// <param name="newModuleTypeId">�µ�ģ������Id��</param>
        public static void MoveModule(string id, string newModuleTypeId)
        {
            Db.SessionFactory.EvictQueries("Module");

            Module m = Db.Session.Load(typeof(Module), id) as Module;
            ModuleType newParent = Db.Session.Load(typeof(ModuleType), newModuleTypeId) as ModuleType;

            m.MoveTo(newParent);

            Db.TransUpdate(m);
        }
        #endregion

    }
}
