using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRIMAS.Repository
{
    /// <summary>
    /// Initiates contracts for recurrent tasks in denari. 
    /// fired immediately after account is created every 30days
    /// </summary>
    public interface IDenariCron
    {
        /// <summary>
        /// customer dividends is credited every month
        /// </summary>
        void initateDividends();

        /// <summary>
        /// Bank Reconciliation form generated monthly
        /// </summary>
        void generateReconciliation();
    }
}
