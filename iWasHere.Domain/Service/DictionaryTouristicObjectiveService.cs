﻿using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class DictionaryTouristicObjectiveService
    {
        private readonly BlackWidowContext _dbContext;
        public DictionaryTouristicObjectiveService(BlackWidowContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<DictionaryAttractionCategoryModel> GetAttraction()
        {
           List<DictionaryAttractionCategoryModel> dictionaryAttraction = _dbContext.DictionaryAttractionCategory.Select(a => new DictionaryAttractionCategoryModel()
            {
                AttractionCategoryId = a.AttractionCategoryId,
                AttractionCategoryName = a.AttractionCategoryName
               
            }).ToList();

            return dictionaryAttraction;
        }        

        public List<DictionaryOpenSeasonModel> GetSeason()
        {
            List<DictionaryOpenSeasonModel> dictionaryOpenSeasons = _dbContext.DictionaryOpenSeason.Select(a => new DictionaryOpenSeasonModel()
            {
                Id = a.OpenSeasonId,
                Type = a.OpenSeasonType
            }).ToList();

            return dictionaryOpenSeasons;
        }

        public List<CityDTO> GetCity()
        {
            List<CityDTO> city = _dbContext.DictionaryCity.Select(a => new CityDTO()
            {
                cityId = a.CityId,
                cityName = a.CityName

            }).ToList();

            return city;
        }

        public List<DictionaryTicketModel> GetTypeTickets()
        {
            List<DictionaryTicketModel> tickets = _dbContext.DictionaryTicket.Select(a => new DictionaryTicketModel()
            {
                DictionaryTicketId = a.DictionaryTicketId,
                TicketCategory = a.TicketCategory

            }).ToList();

            return tickets;           
            
        }

        public List<DictionaryCurrencyTicketDTO> GetCurrency()
        {
            List<DictionaryCurrencyTicketDTO> currency = _dbContext.DictionaryCurrency.Select(a => new DictionaryCurrencyTicketDTO()
            {
                Id = a.DictionaryCurrencyId,
                Currency = a.CurrencyCode
            }).ToList();

            return currency;
        }

        public string UpdateDB(TouristicObjectiveDTO model)
        {
                int id;
           
                TouristicObjective obj = _dbContext.TouristicObjective.Find(model.TouristicObjectiveId);
                obj.TouristicObjectiveName = model.TouristicObjectiveName;
                obj.TouristicObjectiveCode = model.TouristicObjectiveCode;
                obj.TouristicObjectiveDescription = model.TouristicObjectiveDescription;
                obj.HasEntry = model.HasEntry;
                obj.OpenSeasonId = model.OpenSeasonId;
                obj.AttractionCategoryId = model.AttractionCategoryId;
                obj.CityId = model.CityId;
                obj.Latitude = model.Latitude;
                obj.Longitude = model.Longitude;
                _dbContext.TouristicObjective.Update(obj);
                _dbContext.SaveChanges();
                if (model.HasEntry)
                {
                    id = _dbContext.Ticket.Where(a => a.TouristicObjectiveId == obj.TouristicObjectiveId).Select(a => a.TicketId).FirstOrDefault();
                    if (id != 0)
                    {
                        Ticket ticket = _dbContext.Ticket.Find(id);
                        ticket.Price = model.Price;
                        ticket.DictionaryCurrencyId = model.CurrencyId;
                        ticket.DictionaryTicketId = model.DictionaryTicketId;
                        _dbContext.Ticket.Update(ticket);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        Ticket ticket = new Ticket
                        {
                            Price = model.Price,
                            DictionaryCurrencyId = model.CurrencyId,
                            DictionaryTicketId = model.DictionaryTicketId,
                            TouristicObjectiveId = model.TouristicObjectiveId,
                            DictionaryExchangeRateId = 1

                        };

                        _dbContext.Ticket.Add(ticket);
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    id = _dbContext.Ticket.Where(a => a.TouristicObjectiveId == obj.TouristicObjectiveId).Select(a => a.TicketId).FirstOrDefault();
                    if (id != 0)
                    {
                        _dbContext.Remove(_dbContext.Ticket.Single(a => a.TicketId == id));
                        _dbContext.SaveChanges();
                    }
                }
                return null;
            
        }

        public string Update(TouristicObjectiveDTO model)
        {
            if(model.Unique == 0)
            {
                    return UpdateDB(model);
            }
            else
            {
                int code = _dbContext.TouristicObjective.Where(x => x.TouristicObjectiveCode.ToLower() == model.TouristicObjectiveCode.ToLower()).Count();
                if (code == 0)
                {
                    return UpdateDB(model);
                }
                else
                {
                    return "Codul tau nu este unic";
                }
            }
           
                
        }
        

        public string Insert(TouristicObjectiveDTO model)
        {
            if (String.IsNullOrWhiteSpace(model.TouristicObjectiveName))
            {
                return "Numele atractiei este obligatoriu";
            }
            else if (String.IsNullOrWhiteSpace(model.TouristicObjectiveCode))
            {
                return "Cod obligatoriu";
            }
            else if (model.OpenSeasonId == 0)
            {
                return "Sezonului nu este completat";
            }
            else if (model.CityId == 0)
            {
                return "Orasul este obligatoriu";
            }
            else if (model.AttractionCategoryId == 0)
            {
                return "Tipul atractiei este obligatoriu";
            }
            //try
            //{
            int id = _dbContext.TouristicObjective.Where(x => x.TouristicObjectiveCode.ToLower() == model.TouristicObjectiveCode.ToLower()).Count();
                if (id != 0)
                {
                    return "Codul atractiei trebuie sa fie unic";
                }
                else
                {
                    _dbContext.TouristicObjective.Add(new TouristicObjective
                    {
                        TouristicObjectiveDescription = model.TouristicObjectiveDescription,
                        TouristicObjectiveName = model.TouristicObjectiveName,
                        TouristicObjectiveCode = model.TouristicObjectiveCode,
                        HasEntry = model.HasEntry,
                        OpenSeasonId = model.OpenSeasonId,
                        CityId = model.CityId,
                        AttractionCategoryId = model.AttractionCategoryId,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude
                    });
                    _dbContext.SaveChanges();
                    if (model.HasEntry)
                    {
                        model.TouristicObjectiveId = _dbContext.TouristicObjective.Where(x => x.TouristicObjectiveCode.ToLower() == model.TouristicObjectiveCode.ToLower()).Select(x => x.TouristicObjectiveId).FirstOrDefault();
                    
                       
                        _dbContext.Ticket.Add(new Ticket
                        {
                            Price = model.Price,
                            DictionaryCurrencyId = model.CurrencyId,
                            DictionaryTicketId = model.DictionaryTicketId,
                            DictionaryExchangeRateId = 1,
                            TouristicObjectiveId = model.TouristicObjectiveId
                        });
                        _dbContext.SaveChanges();
                    }
                    return null;
                    
                }
            //}catch(Exception e)
            //{
            //    return "Ceva a mers prost";
            //}
        }

        public TouristicObjectiveDTO GetTouristicObjectiveById(int id)
        {
           
            TouristicObjectiveDTO obj = _dbContext.TouristicObjective
               .Where(a => a.TouristicObjectiveId == id)
                .Select(a => new TouristicObjectiveDTO()
                {
                    TouristicObjectiveId = a.TouristicObjectiveId,
                    TouristicObjectiveCode = a.TouristicObjectiveCode,
                    TouristicObjectiveName = a.TouristicObjectiveName,
                    TouristicObjectiveDescription = a.TouristicObjectiveDescription,
                    HasEntry = a.HasEntry,
                    AttractionCategoryId = a.AttractionCategoryId,
             
                    OpenSeasonId = a.OpenSeasonId,
                    CityId = a.CityId,
                    Longitude = a.Longitude,
                    Latitude = a.Latitude

                }).First();
           
            obj.cityName = _dbContext.DictionaryCity.Where(a => a.CityId == obj.CityId).Select(a => a.CityName).FirstOrDefault();
            obj.AttractionCategoryName = _dbContext.DictionaryAttractionCategory.Where(a => a.AttractionCategoryId == obj.AttractionCategoryId).Select
                (a => a.AttractionCategoryName).FirstOrDefault();
            obj.Type = _dbContext.DictionaryOpenSeason.Where(a => a.OpenSeasonId == obj.OpenSeasonId).Select(a => a.OpenSeasonType).FirstOrDefault();
            if(obj.HasEntry == true)
            {
                obj.Price = _dbContext.Ticket.Where(a => a.TouristicObjectiveId == obj.TouristicObjectiveId).Select(a => a.Price).FirstOrDefault();
                obj.DictionaryTicketId = _dbContext.Ticket.Where(a => a.TouristicObjectiveId == obj.TouristicObjectiveId).Select(a => a.DictionaryTicketId).FirstOrDefault();
                obj.CurrencyId = _dbContext.Ticket.Where(a => a.TouristicObjectiveId == obj.TouristicObjectiveId).Select(a => a.DictionaryCurrencyId).FirstOrDefault();
                obj.TicketCategory = _dbContext.DictionaryTicket.Where(x => x.DictionaryTicketId == obj.DictionaryTicketId).Select(x => x.TicketCategory).FirstOrDefault();
                obj.Currency = _dbContext.DictionaryCurrency.Where(x => x.DictionaryCurrencyId == obj.CurrencyId).Select(x => x.CurrencyCode).FirstOrDefault();
            }

            return obj;
        }

      


    }

    }
