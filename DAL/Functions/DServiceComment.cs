using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using Entity;
using Microsoft.EntityFrameworkCore;
using static DAL.DALException;

namespace DAL.Functions
{
    public class DServiceComment
    {

        public List<EServiceComment> getAllServiceComment(int ServiceId)
        {
            List<EServiceComment> lServiceComment = new List<EServiceComment>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select *,CONCAT(U.FirstName,U.Lastname) as FullName from ServiceComment C ");
                    sQuery.Append(" inner join Users u On U.UserId=C.CommentBy ");
                    sQuery.AppendFormat(" where C.ServiceId={0} and C.EndDate is null order by C.CommentDate desc", ServiceId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceComment oEServiceComment = new EServiceComment();
                            if (dataReader["ServiceCommentId"] != DBNull.Value) { oEServiceComment.ServiceCommentId = (int)dataReader["ServiceCommentId"]; }
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceComment.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["Comment"] != DBNull.Value) { oEServiceComment.Comment = (string)dataReader["Comment"]; }
                            if (dataReader["CommentBy"] != DBNull.Value) { oEServiceComment.CommentBy = (int)dataReader["CommentBy"]; }
                            if (dataReader["CommentDate"] != DBNull.Value) { oEServiceComment.CommentDate = (DateTime)dataReader["CommentDate"]; }
                            if (dataReader["FullName"] != DBNull.Value) { oEServiceComment.FullName = (string)dataReader["FullName"]; }

                            lServiceComment.Add(oEServiceComment);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lServiceComment;
        }


        public EServiceComment getSingleServiceComment(int commentId)
        {
 
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EServiceComment oEServiceComment=null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select * from ServiceComment C ");
                    sQuery.AppendFormat(" where C.commentId={0} ", commentId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                             oEServiceComment = new EServiceComment();
                            if (dataReader["ServiceCommentId"] != DBNull.Value) { oEServiceComment.ServiceCommentId = (int)dataReader["ServiceCommentId"]; }
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceComment.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["Comment"] != DBNull.Value) { oEServiceComment.Comment = (string)dataReader["Comment"]; }
                            if (dataReader["CommentBy"] != DBNull.Value) { oEServiceComment.CommentBy = (int)dataReader["CommentBy"]; }
                            if (dataReader["CommentDate"] != DBNull.Value) { oEServiceComment.CommentDate = (DateTime)dataReader["CommentDate"]; }

                           
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEServiceComment;
        }


        public async Task<EServiceComment> addComment(EServiceComment newComment)
        {

            newComment.CommentDate=  DateTime.UtcNow;

            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.ServiceComment.AddAsync(newComment);
                await context.SaveChangesAsync();
            }

            return newComment;
        }
        public async Task<EServiceComment> updateComment(EServiceComment comment)
        {
          
          
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.ServiceComment.Attach(comment);
                context.Entry(comment).Property(x => x.Comment).IsModified = true;
                

                await context.SaveChangesAsync();
            }

            return comment;
        }

        public async Task<EServiceComment> removeComment(int id)
        {
            EServiceComment eBranch = new EServiceComment();

            eBranch=getSingleServiceComment(id);
  
            eBranch.EndDate =  DateTime.UtcNow;

            if (eBranch == null)
            {
                throw new DomainValidationFundException("Validation : The Branch is not found, make sure you are removing the correct Branch");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.ServiceComment.Attach(eBranch);
                context.Entry(eBranch).Property(x => x.EndDate).IsModified = true;

                await context.SaveChangesAsync();
            }

            return eBranch;
        }
    }
}
