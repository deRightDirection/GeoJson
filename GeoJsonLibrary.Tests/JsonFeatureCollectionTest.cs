﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace GeoJsonLibrary.Tests
{
    [TestClass]
    public class JsonFeatureCollectionTest
    {
        private TestEntity _entity;
        private TestEntity3 _entityWithPropertyAttributeGeography;
        private GeoJsonFeatureCollection _featureCollection;

        [TestMethod]
        public void Convert_To_GeoJson_List()
        {
            var list = new List<TestEntity>() { _entity, _entity };
            _featureCollection.AddFeatures<List<TestEntity>>(list);
            var entity = _featureCollection.Features;
            Assert.AreEqual(2, entity.Count);
        }

        [TestMethod]
        public void Convert_To_GeoJson_List_As_Json()
        {
            var list = new List<TestEntity>() { _entity, _entity };
            _featureCollection.AddFeatures<List<TestEntity>>(list);
            var result = _featureCollection.ToJson();
            var json = "{\"type\":\"FeatureCollection\",\"features\":[{\"geometry\":{\"type\":\"Point\",\"coordinates\":[10.0,10.0],\"crs\":{\"type\":\"name\",\"properties\":{\"name\":\"EPSG:4326\"}}},\"type\":\"Feature\",\"properties\":{\"name\":\"b\",\"subentity\":{\"Name\":\"TestEntity2\",\"Value\":100},\"subgeometries\":[{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null}],\"value\":1,\"values\":[\"a\",\"b\",\"c\"]}},{\"geometry\":{\"type\":\"Point\",\"coordinates\":[10.0,10.0],\"crs\":{\"type\":\"name\",\"properties\":{\"name\":\"EPSG:4326\"}}},\"type\":\"Feature\",\"properties\":{\"name\":\"b\",\"subentity\":{\"Name\":\"TestEntity2\",\"Value\":100},\"subgeometries\":[{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null}],\"value\":1,\"values\":[\"a\",\"b\",\"c\"]}}]}";
            Assert.AreEqual(json, result);
        }

        [TestMethod]
        public void Convert_To_GeoJson_List_As_Json_With_Rename_Of_Property()
        {
            var list = new List<TestEntity5>() { new TestEntity5() { Geometry = GeoUtils.CreatePoint(10, 10) } };
            _featureCollection.AddFeatures<List<TestEntity5>>(list);
            var result = _featureCollection.ToJson();
            var json = "{\"type\":\"FeatureCollection\",\"features\":[{\"geometry\":{\"type\":\"Point\",\"coordinates\":[10.0,10.0],\"crs\":{\"type\":\"name\",\"properties\":{\"name\":\"EPSG:4326\"}}},\"type\":\"Feature\",\"properties\":{\"waarde\":0,\"getal\":0}}]}";
            Assert.AreEqual(json, result);
        }

        [TestMethod]
        public void Convert_To_GeoJson_One_Object_With_Class_Attribute_Geography()
        {
            _featureCollection.AddFeatures<TestEntity>(_entity);
            var entity = _featureCollection.Features;
            Assert.AreEqual(1, entity.Count);
        }

        [TestMethod]
        public void Convert_To_GeoJson_One_Object_With_Class_Attribute_Geography_As_Json()
        {
            _featureCollection.AddFeatures<TestEntity>(_entity);
            var result = _featureCollection.ToJson();
            var json = "{\"type\":\"FeatureCollection\",\"features\":[{\"geometry\":{\"type\":\"Point\",\"coordinates\":[10.0,10.0],\"crs\":{\"type\":\"name\",\"properties\":{\"name\":\"EPSG:4326\"}}},\"type\":\"Feature\",\"properties\":{\"name\":\"b\",\"subentity\":{\"Name\":\"TestEntity2\",\"Value\":100},\"subgeometries\":[{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null}],\"value\":1,\"values\":[\"a\",\"b\",\"c\"]}}]}";
            Assert.AreEqual(json, result);
        }

        [TestMethod]
        public void Convert_To_GeoJson_One_Object_With_Property_Attribute_Geography()
        {
            _featureCollection.AddFeatures<TestEntity3>(_entityWithPropertyAttributeGeography);
            var entity = _featureCollection.Features;
            Assert.AreEqual(1, entity.Count);
        }

        [TestMethod]
        public void Convert_To_GeoJson_One_Object_With_Property_Attribute_Geography_As_Json()
        {
            _featureCollection.AddFeatures<TestEntity3>(_entityWithPropertyAttributeGeography);
            var result = _featureCollection.ToJson();
            var json = "{\"type\":\"FeatureCollection\",\"features\":[{\"geometry\":{\"type\":\"Point\",\"coordinates\":[10.0,10.0],\"crs\":{\"type\":\"name\",\"properties\":{\"name\":\"EPSG:4326\"}}},\"type\":\"Feature\",\"properties\":{\"name\":\"b\",\"subentity\":{\"Name\":\"TestEntity2\",\"Value\":100},\"subgeometries\":[{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null}],\"value\":1,\"values\":[\"a\",\"b\",\"c\"]}}]}";
            Assert.AreEqual(json, result);
        }

        [TestMethod]
        public void Convert_To_GeoJson_With_Two_Lists()
        {
            var list = new List<TestEntity>() { _entity, _entity };
            var list2 = new List<TestEntity3>() { _entityWithPropertyAttributeGeography, _entityWithPropertyAttributeGeography };
            _featureCollection.AddFeatures<List<TestEntity>>(list);
            _featureCollection.AddFeatures<List<TestEntity3>>(list2);
            var entity = _featureCollection.Features;
            Assert.AreEqual(4, entity.Count);
        }

        [TestMethod]
        public void Convert_To_GeoJson_With_Two_Lists_As_Json()
        {
            var list = new List<TestEntity>() { _entity, _entity };
            var list2 = new List<TestEntity3>() { _entityWithPropertyAttributeGeography, _entityWithPropertyAttributeGeography };
            _featureCollection.AddFeatures<List<TestEntity>>(list);
            _featureCollection.AddFeatures<List<TestEntity3>>(list2);
            var result = _featureCollection.ToJson();
            var json = "{\"type\":\"FeatureCollection\",\"features\":[{\"geometry\":{\"type\":\"Point\",\"coordinates\":[10.0,10.0],\"crs\":{\"type\":\"name\",\"properties\":{\"name\":\"EPSG:4326\"}}},\"type\":\"Feature\",\"properties\":{\"name\":\"b\",\"subentity\":{\"Name\":\"TestEntity2\",\"Value\":100},\"subgeometries\":[{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null}],\"value\":1,\"values\":[\"a\",\"b\",\"c\"]}},{\"geometry\":{\"type\":\"Point\",\"coordinates\":[10.0,10.0],\"crs\":{\"type\":\"name\",\"properties\":{\"name\":\"EPSG:4326\"}}},\"type\":\"Feature\",\"properties\":{\"name\":\"b\",\"subentity\":{\"Name\":\"TestEntity2\",\"Value\":100},\"subgeometries\":[{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null}],\"value\":1,\"values\":[\"a\",\"b\",\"c\"]}},{\"geometry\":{\"type\":\"Point\",\"coordinates\":[10.0,10.0],\"crs\":{\"type\":\"name\",\"properties\":{\"name\":\"EPSG:4326\"}}},\"type\":\"Feature\",\"properties\":{\"name\":\"b\",\"subentity\":{\"Name\":\"TestEntity2\",\"Value\":100},\"subgeometries\":[{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null}],\"value\":1,\"values\":[\"a\",\"b\",\"c\"]}},{\"geometry\":{\"type\":\"Point\",\"coordinates\":[10.0,10.0],\"crs\":{\"type\":\"name\",\"properties\":{\"name\":\"EPSG:4326\"}}},\"type\":\"Feature\",\"properties\":{\"name\":\"b\",\"subentity\":{\"Name\":\"TestEntity2\",\"Value\":100},\"subgeometries\":[{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null},{\"Value\":2,\"Values\":[\"d\",\"e\",\"f\"],\"Name\":\"c\",\"NotInUse\":0,\"SubEntity\":null}],\"value\":1,\"values\":[\"a\",\"b\",\"c\"]}}]}";
            Assert.AreEqual(json, result);
        }

        [TestInitialize]
        public void Setup()
        {
            var subEntity = new TestEntity4()
            {
                Name = "c",
                Value = 2,
                NotInUse = 0,
                Values = new List<string>() { "d", "e", "f" },
            };
            var list = new List<TestEntity4>() { subEntity, subEntity, subEntity };
            _entity = new TestEntity()
            {
                Geometry = GeoUtils.CreatePoint(10, 10),
                Name = "b",
                Value = 1,
                NotInUse = 0,
                Values = new List<string>() { "a", "b", "c" },
                SubEntity = new TestEntity2(),
                SubGeometries = list
            };
            _entityWithPropertyAttributeGeography = new TestEntity3()
            {
                Geometry = GeoUtils.CreatePoint(10, 10),
                Name = "b",
                Value = 1,
                NotInUse = 0,
                Values = new List<string>() { "a", "b", "c" },
                SubEntity = new TestEntity2(),
                SubGeometries = list
            };
            _featureCollection = new GeoJsonFeatureCollection();
        }
    }
}