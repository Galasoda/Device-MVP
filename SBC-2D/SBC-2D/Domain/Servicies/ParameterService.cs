using SBC_2D.Infrastructures;
using SBC_2D.Infrastructures.Logger;
using SBC_2D.Infrastructures.Parameter;
using SBC_2D.Shared;
using System;
using System.Collections.Generic;

namespace SBC_2D.Servicies
{
    public class ParameterService
    {
        private ParameterStore _store;
        private ParameterRepository _parameterRepository;

        public ParameterService(ParameterStore store, ParameterRepository parameterRepository)
        {
            _store = store;
            _parameterRepository = parameterRepository;
        }

        public List<string> GetNames()
        {
            try
            {
                List<string> names = _parameterRepository.SelectNames();
                LoggerStore.RecordSystem(LogType.Info, $"Get total of {names.Count} model names.");
                LoggerStore.RecordCodeTrace(LogType.Debug, $"Completed {names.Count}.");
                return names;
            }
            catch (Exception ex)
            {
                LoggerStore.RecordSystem(LogType.Info, $"Failed to get model names.");
                LoggerStore.RecordCodeTrace(LogType.Warn, $"Exception, {ex.Message}");
                return null;
            }
        }

        public Parameters GetModel(string name)
        {
            try
            {
                Parameters model = _parameterRepository.SelectByName(name);
                LoggerStore.RecordSystem(LogType.Info, $"Geted model {name}.");
                LoggerStore.RecordCodeTrace(LogType.Debug, $"Completed {name}.");
                return model;
            }
            catch (Exception ex)
            {
                LoggerStore.RecordSystem(LogType.Info, $"Failed to get model {name ?? "null"}.");
                LoggerStore.RecordCodeTrace(LogType.Warn, $"Exception, {ex.Message}");
                return null;
            }
        }

        public bool UpdateModel(Parameters model)
        {
            try
            {
                if(model == null)
                {
                    LoggerStore.RecordSystem(LogType.Info, $"Failed to update a null model.");
                    LoggerStore.RecordCodeTrace(LogType.Debug, $"Failed to update a null model.");
                    return false;
                }
                bool isUpdated = _parameterRepository.Update(model);
                if (!isUpdated)
                {
                    LoggerStore.RecordSystem(LogType.Info, $"Failed to update model {model.Name}");
                    LoggerStore.RecordCodeTrace(LogType.Debug, $"Failed to update model {model.Name}");
                }
                else
                {
                    LoggerStore.RecordSystem(LogType.Info, $"Updated model {model.Name}");
                    LoggerStore.RecordCodeTrace(LogType.Debug, $"Completed {model.Name}");
                }
                return isUpdated;
            }
            catch (Exception ex)
            {
                LoggerStore.RecordSystem(LogType.Info, $"Failed to update model {model.Name}");
                LoggerStore.RecordCodeTrace(LogType.Warn, $"Exception {ex.Message}");
                return false;
            }
        }

        public bool UpdateModel(string name, string columnName, object value)
        {
            try
            {
                bool isUpdated = _parameterRepository.Update(name, columnName, value);
                if (!isUpdated)
                {
                    LoggerStore.RecordSystem(LogType.Info, $"Failed to update model {name}, column {columnName}: {value}");
                    LoggerStore.RecordCodeTrace(LogType.Debug, $"Failed to update model {name}, column {columnName}: {value}");
                }
                else
                {
                    LoggerStore.RecordSystem(LogType.Info, $"Updated model {name}, column {columnName}: {value}");
                    LoggerStore.RecordCodeTrace(LogType.Debug, $"Completed {name}, {columnName}: {value}");
                }
                return isUpdated;
            }
            catch (Exception ex)
            {
                LoggerStore.RecordSystem(LogType.Info, $"Failed to update model {name}, column {columnName}: {value}");
                LoggerStore.RecordCodeTrace(LogType.Warn, $"Exception {ex.Message}");
                return false;
            }
        }

        public bool CreateModel(Parameters model)
        {
            try
            {
                if(model == null)
                {
                    LoggerStore.RecordSystem(LogType.Info, $"Failed to create a null model.");
                    LoggerStore.RecordCodeTrace(LogType.Debug, $"Failed to create a null model.");
                    return false;
                }
                bool isCreated = _parameterRepository.Create(model);
                if (!isCreated)
                {
                    LoggerStore.RecordSystem(LogType.Info, $"Failed to create model {model.Name}");
                    LoggerStore.RecordCodeTrace(LogType.Debug, $"Failed to create model {model.Name}");
                }
                else
                {
                    LoggerStore.RecordSystem(LogType.Info, $"Created model {model.Name}");
                    LoggerStore.RecordCodeTrace(LogType.Debug, $"Completed {model.Name}");
                }
                return isCreated;
            }
            catch (Exception ex)
            {
                LoggerStore.RecordSystem(LogType.Info, $"Failed to create model {model.Name}");
                LoggerStore.RecordCodeTrace(LogType.Warn, $"Exception {ex.Message}");
                return false;
            }
        }
        public bool DeleteModel(string name)
        {
            try
            {
                bool isDelete = _parameterRepository.Delete(name);
                if (!isDelete)
                {
                    LoggerStore.RecordSystem(LogType.Info, $"Failed to delete model {name}");
                    LoggerStore.RecordCodeTrace(LogType.Debug, $"Failed to delete model {name}");
                }
                else
                {
                    LoggerStore.RecordSystem(LogType.Info, $"Deleted model model {name}");
                    LoggerStore.RecordCodeTrace(LogType.Debug, $"Completed {name}");
                }
                return isDelete;
            }
            catch (Exception ex)
            {
                LoggerStore.RecordSystem(LogType.Info, $"Failed to create model {name}");
                LoggerStore.RecordCodeTrace(LogType.Warn, $"Exception {ex.Message}");
                return false;
            }
        }
    }
}
