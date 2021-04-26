using System;
using System.Collections.Concurrent;
using System.Threading;
using NHibernate;

namespace TransportSchedulerShared.NHibernateUtilities
{
    /// <summary>
    /// Obsługa sesji NHibernate.
    /// </summary>
    public class SessionManager
    {
        private readonly ConcurrentDictionary<Thread, ISession> sessions = new ConcurrentDictionary<Thread, ISession>();
        private readonly ConcurrentDictionary<Thread, ITransaction> transactions = new ConcurrentDictionary<Thread, ITransaction>();

        private readonly ISessionFactory sessionFactory;

        /// <summary>
        /// Zapamiętuje SessionFactory.
        /// </summary>
        /// <param name="sessionFactory"></param>
        public SessionManager(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        /// <summary>
        /// Zwraca sesję związaną z bieżącym wątkiem lub tworzy nową.
        /// </summary>
        public ISession Session
        {
            get
            {
                try
                {
                    var thread = Thread.CurrentThread;

                    if (sessions.ContainsKey(thread))
                    {
                        var sessionTmp = sessions[thread];

                        if (!sessionTmp.IsOpen)
                        {
                            sessions.TryRemove(thread, out _);
                            transactions.TryRemove(thread, out _);
                        }
                    }

                    if (sessions.ContainsKey(thread))
                    {
                        if (transactions.ContainsKey(thread))
                        {
                            ITransaction transaction = transactions[thread];

                            if (!transaction.IsActive)
                            {
                                transaction.Dispose();
                                transactions.TryRemove(thread, out transaction);

                                transaction = sessions[thread].BeginTransaction();
                                transactions[thread] = transaction;
                            }

                            return sessions[thread];
                        }
                        else
                        {
                            ITransaction transaction = sessions[thread].BeginTransaction();

                            transactions[thread] = transaction;

                            return sessions[thread];
                        }
                    }
                    else
                    {
                        ISession session = sessionFactory.OpenSession();
                        ITransaction transaction = session.BeginTransaction();

                        sessions[thread] = session;
                        transactions[thread] = transaction;

                        return session;
                    }
                }
                catch (Exception exception)
                {
                    //throw exception;
                    string _msg = exception.Message;
                    return null;

                }
            }
        }

        /// <summary>
        /// Zwraca tranzakcję związaną z bieżącym wątkiem.
        /// </summary>
        public ITransaction Transaction
        {
            get
            {
                var thread = Thread.CurrentThread;

                ISession session = Session;

                return transactions[thread];
            }
        }

        /// <summary>
        /// Commituje tranzakcję związaną z bieżącym wątkiem.
        /// </summary>
        public void Commit()
        {
            try
            {
                var thread = Thread.CurrentThread;

                if (transactions.ContainsKey(thread))
                {
                    ITransaction transaction = transactions[thread];

                    if (transaction.IsActive == true)
                    {
                        transaction.Commit();
                    }

                    transaction.Dispose();
                    transactions.TryRemove(thread, out transaction);
                }

                if (sessions.ContainsKey(thread))
                {
                    ISession session = sessions[thread];

                    if (session.IsOpen == true)
                    {
                        session.Close();
                    }

                    session.Dispose();

                    sessions.TryRemove(thread, out session);
                }
            }
            catch (Exception exception)
            {
                // throw exception;
                string _msg = exception.Message;
            }
        }

        /// <summary>
        /// Rollbackuje tranzakcję związaną z bieżącym wątkiem.
        /// </summary>
        public void Rollback()
        {
            var thread = Thread.CurrentThread;

            if (transactions.ContainsKey(thread))
            {
                ITransaction transaction = transactions[thread];

                if (transaction.IsActive == true)
                {
                    transaction.Rollback();
                }

                transaction.Dispose();
                transactions.TryRemove(thread, out transaction);
            }

            if (sessions.ContainsKey(thread))
            {
                ISession session = sessions[thread];

                if (session.IsOpen == true)
                {
                    session.Close();
                }

                session.Dispose();

                sessions.TryRemove(thread, out session);
            }
        }

        public ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }

        public void CloseSesions()
        {
            if (sessions == null || sessions.Count == 0)
                return;

            foreach(var session in sessions)
            {
                CloseSession(session.Value);
            }
        }

        public void CloseSession(ISession session)
        {
            if (session.Transaction.IsActive)
            {
                session.Transaction.Commit();
            }

            session.Flush();
            session.Clear();
            session.Close();
        }

        ~SessionManager()
        {
            CloseSesions();
        }
    }
}
