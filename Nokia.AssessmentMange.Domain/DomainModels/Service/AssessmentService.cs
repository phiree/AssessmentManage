using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nokia.AssessmentMange.Domain.Application.Dtos;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;

namespace Nokia.AssessmentMange.Domain.DomainModels.Service
{
    public class AssessmentService : IAssessmentService
    {

        IAssessmentRepository _assessmentRepository;
        IGradeCalculater gradeCalculater;
        IDepartmentRepository _departmentRepository;
        public AssessmentService(IGradeCalculater gradeCalculater, IAssessmentRepository assessmentRepository, IDepartmentRepository departmentRepository)
        {
            this.gradeCalculater = gradeCalculater;
            this._assessmentRepository = assessmentRepository;
            this._departmentRepository = departmentRepository;
        }

        public SearchPageVO<Assessment> GetList(string departmentID, int pageSize, int pageCurrent)
        {
            SearchPageVO<Assessment> result = new SearchPageVO<Assessment>();
            int rowCount = 0;
            var list = _assessmentRepository.GetList(departmentID, pageSize, pageCurrent, out rowCount);
            if (rowCount > 0)
            {
                IEnumerable<Department> deptList = _departmentRepository.GetAll();
                //需要获取部门的名称， 根目录->本节点名称
                foreach (var assessment in list)
                {
                    assessment.DepartmentRootName = recursionDeptName(deptList, assessment.DepartmentId, null);
                }
            }
            result.DataList = list;
            result.PageCurrent = pageCurrent;
            result.PageSize = pageSize;
            result.RowCount = rowCount;
            return result;
        }

        public Assessment GetAssessment(string assessmentID)
        {
            return _assessmentRepository.GetAssessment(assessmentID);
        }

        /// <summary>
        /// 保存用户得分.
        /// </summary>
        /// <param name="assessment"></param>
        /// <param name="person"></param>
        /// <param name="grades"></param>
        public void SavePersonGrade(Assessment assessment, Person person, bool isAbsent, bool isMakeup, IEnumerable<SubjectGrade> grades)
        {
            foreach (var subjectGrade in grades)
            {
                var subject = subjectGrade.Subject;
                //if (!assessment.Contains(subject.Id))
                //{
                //    throw new Exceptions.AssessmentNotContainSubject(assessment.Name, subject.Name);
                //}
                gradeCalculater.CalculateGrade(subjectGrade, grades);
            }


            //PersonAssessmentGrade personGrade = new PersonAssessmentGrade(assessment.Id, person.Id, isAbsent, isMakeup, grades);

        }


        private string recursionDeptName(IEnumerable<Department> deptList, string deptID, string name)
        {
            Department department = deptList.FirstOrDefault(item => item.Id == deptID);
            if (department == null)
            {
                return name;
            }
            name = string.Concat(department.Name, (string.IsNullOrEmpty(name) ? "" : "-"), name);
            if (string.IsNullOrEmpty(department.ParentId))
            {
                return name;
            }
            else
            {
                return recursionDeptName(deptList, department.Id, name);
            }
        }
    }
}
