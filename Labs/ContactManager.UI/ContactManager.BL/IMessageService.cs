/*
 * Lab 3
 * Matthew McNatt
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.BL
{
    public interface IMessageService
    {
        void Send(Message message);
    }
}
