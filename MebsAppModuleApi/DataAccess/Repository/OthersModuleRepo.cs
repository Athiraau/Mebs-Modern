using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Oracle;
using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess.Repository
{
    public class OthersModuleRepo : IOthersModuleRepo
    {
        private DataContext _context; 
        private readonly DtoWrapper _dto;
        public OthersModuleRepo (DataContext context,DtoWrapper dto)
        {
            _context = context;
            _dto = dto;
           
        }

        public async Task<dynamic> GetOthersData(string flag, string pagevalue, string paravalue)
        {
            OracleRefCursor result = null;
            var procedureName = "proc_mebs_others_get";   
            var parameters = new OracleDynamicParameters();
            parameters.Add("p_flag", flag, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_pageval", pagevalue, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_paraval", paravalue, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_as_outresult", result, OracleMappingType.RefCursor, ParameterDirection.Output);


            parameters.BindByName = true;
            using var connection = _context.CreateConnection();
            var response = await connection.QueryAsync<dynamic>
                (procedureName, parameters, commandType: CommandType.StoredProcedure);
            return response;
           

        }


        public async Task<dynamic> PostOthersData(PostReqDto PostReq)
        {
            OracleRefCursor result = null;
            var procedureName = "PROC_MEBS_OTHERS_POST";
            var parameters = new OracleDynamicParameters();
            parameters.Add("p_flag", PostReq.p_flag, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_pageval", PostReq.p_pagevalue, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_paraval", PostReq.p_paravalue, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_as_outresult", result, OracleMappingType.RefCursor, ParameterDirection.Output);

            parameters.BindByName = true;
            using var connection = _context.CreateConnection();
            var response = await connection.QueryAsync<dynamic>
                (procedureName, parameters, commandType: CommandType.StoredProcedure);
            return response;

        }


        public async Task<dynamic> UploadDocument(DocUploadPostDto docUploadDto)
        {
            using var connection = _context.CreateConnection();
                       
            string query =docUploadDto.p_query;

            var query1 = Convert.ToString(query); 

            connection.Open();
            OracleParameter[] prm = new OracleParameter[1];
            OracleCommand cmd = (OracleCommand)connection.CreateCommand();

            prm[0] = cmd.Parameters.Add("DOCUMENT", OracleDbType.Blob, docUploadDto.DocData, ParameterDirection.Input);

            cmd.CommandText = query1;
            cmd.ExecuteNonQuery();

            return "1";
           
        }

    }
}


