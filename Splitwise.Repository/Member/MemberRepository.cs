using Splitwise.Data;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository
{
    public class MemberRepository : IMemberRepository<MemberDTO>
    {
        #region Contructor

        public MemberRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion
        #region Private Varibles

        private readonly AppDbContext _dbContext;
        #endregion
        #region Public Methods
        public void AddMember() { 
        }

        public IEnumerable<MemberDTO> AllMember()
        {
            throw new NotImplementedException();
        }

        public void DeleteMember()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
